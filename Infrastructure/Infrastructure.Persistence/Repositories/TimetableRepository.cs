using Application.Common.Interfaces.Repositories;
using Application.Position.DTOs.ResponseDTOs;
using Application.TimetableTemplate.DTOs;
using Application.TimetableTemplate.DTOs.Request;
using Application.TimetableTemplate.DTOs.Response;
using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infrastructure.Persistence.Repositories
{
    internal class TimetableRepository : ITimetableRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly DateOnly _defaultDateOnly = new DateOnly(2024, 01, 01);

        public TimetableRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AddTimetableTemplateDto> CreateTemplateAsync(AddTimetableTemplateDto request, CancellationToken cancellationToken)
        {
            var result = await CreateTimetableTemplateAsync(request, cancellationToken);

            return result;
        }

        public async Task<AddTimetableTemplateDto> CreateDefaultTemplateAsync(Guid physicianId, CancellationToken cancellationToken)
        {
            var startDate = DateOnly.FromDateTime(DateTime.UtcNow);
            var endDate = startDate.AddDays(7);

            var sessionTemplates = GetDefaultSessions();

            var request = new AddTimetableTemplateDto(startDate, endDate,
                GetDefaultTimetableTemplate(new DaySessionsDto(true, sessionTemplates)),
                physicianId);

            var result = await CreateTimetableTemplateAsync(request, cancellationToken);

            return result;
        }

        public async Task<GetTimetablesDto?> GetTimetablesAsync(GetTimetableTemplateDto request, CancellationToken cancellationToken)
        {
            var timetableTemplate = await _dbContext.TimetableTemplates
                    .FirstOrDefaultAsync(t => t.PhysicianDataId == request.PhysicianId,
                    cancellationToken);

            if (timetableTemplate is null)
            {
                return null;
            }

            var weeklyTemplates =
                await GetTemplatesDictionaryAsync(timetableTemplate.Id, cancellationToken);

            var sessionsInTimetable = await _dbContext.Sessions.Where(s => s.PhysicianDataId == request.PhysicianId &&
                                                                     DateOnly.FromDateTime(s.StartTime) >= timetableTemplate.StartDate &&
                                                                     DateOnly.FromDateTime(s.EndTime) <= timetableTemplate.EndDate)
                                                                     .ToListAsync(cancellationToken);

            var isEditable = !sessionsInTimetable.Any(s => IsSessionTemplateMatch(weeklyTemplates, s));

            var result = new GetTimetablesDto(timetableTemplate.StartDate, timetableTemplate.EndDate,
                weeklyTemplates, request.PhysicianId, isEditable);
            return result;
        }

        public async Task<GetAvailableTimetableDto?> GetAvailableTimetableAsync(Guid physicianId, CancellationToken cancellationToken)
        {
            var availableTimetable = new Dictionary<DateOnly, List<SessionTimeTemplateDto>>();

            var timetableTemplate = await _dbContext.TimetableTemplates.SingleOrDefaultAsync
            (
                t => t.PhysicianDataId == physicianId, 
                cancellationToken
            );

            if (timetableTemplate is null)
            {
                return null;
            }

            var activeSessionDays = await GetActiveSessionDayTemplatesAsync(timetableTemplate.Id, cancellationToken);
            var assignedSessions = await _dbContext.Sessions
                .Where(s => s.PhysicianDataId == physicianId)
                .ToListAsync(cancellationToken);

            var timetableDates = EnumerateDates
                (
                    timetableTemplate.StartDate,
                    timetableTemplate.EndDate,
                    activeSessionDays.Select(st => st.DayOfWeek).ToList()
                )
                .Where(d => d >= DateOnly.FromDateTime(DateTime.Today))
                .ToList();
             
            foreach (var timetableDate in timetableDates)
            {
                var availableSessionsForDay = GetAvailableSessionsForDay(timetableDate, activeSessionDays, assignedSessions);

                if (availableSessionsForDay.Any())
                {
                    availableTimetable.Add(timetableDate, availableSessionsForDay);
                }
            }

            var physician = await GetPhysicianInfoAsync(physicianId, cancellationToken);
            var result = new GetAvailableTimetableDto(availableTimetable, physician);

            return result;
        }

        public async Task<UpdateTimetableTemplateDto> UpdateTemplateAsync(UpdateTimetableTemplateDto request, CancellationToken cancellationToken)
        {
            var timetableTemplate = await _dbContext.TimetableTemplates
                .SingleAsync(t => t.PhysicianDataId == request.PhysicianDataId, cancellationToken);

            var sessionDays = await _dbContext.SessionTemplateDays
                .Include(s => s.SessionTemplateTimes)
                .Where(s => s.TimetableTemplateId == timetableTemplate.Id)
                .ToListAsync(cancellationToken);

            _dbContext.SessionTemplateDays.RemoveRange(sessionDays);

            timetableTemplate.StartDate = request.StartDate;
            timetableTemplate.EndDate = request.EndDate;

            _dbContext.TimetableTemplates.Update(timetableTemplate);

            var sessionDaysUpdate = CreateSessionDays(request.SessionTemplates, timetableTemplate.Id);
            var sessionTemplateTimesUpdate = CreateSessionTemplateTimes(sessionDaysUpdate, request.SessionTemplates);

            await _dbContext.SessionTemplateDays.AddRangeAsync(sessionDaysUpdate, cancellationToken);
            await _dbContext.SessionTemplateTimes.AddRangeAsync(sessionTemplateTimesUpdate, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var result = new UpdateTimetableTemplateDto
            (
                request.StartDate, 
                request.EndDate, 
                request.SessionTemplates,
                request.PhysicianDataId
            );

            return result;
        }

        public async Task<bool> DeleteTemplateAsync(Guid physicianId, CancellationToken cancellationToken)
        {
            var timetableTemplate = await _dbContext.TimetableTemplates
                    .FirstOrDefaultAsync(t => t.PhysicianDataId == physicianId,
                    cancellationToken);

            if (timetableTemplate is null)
            {
                return false;
            }

            _dbContext.TimetableTemplates.Remove(timetableTemplate);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }

        private async Task<AddTimetableTemplateDto> CreateTimetableTemplateAsync(AddTimetableTemplateDto request,
            CancellationToken cancellationToken)
        {
            var timetableTemplate = await _dbContext.TimetableTemplates.AddAsync(new TimetableTemplate
            {
                Id = Guid.NewGuid(),
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                PhysicianDataId = request.PhysicianDataId
            }, cancellationToken);

            var sessionDays = CreateSessionDays(request.SessionTemplates, timetableTemplate.Entity.Id);
            var sessionTemplateTimes = CreateSessionTemplateTimes(sessionDays, request.SessionTemplates);

            await _dbContext.SessionTemplateDays.AddRangeAsync(sessionDays, cancellationToken);
            await _dbContext.SessionTemplateTimes.AddRangeAsync(sessionTemplateTimes, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var response = new AddTimetableTemplateDto(request.StartDate, request.EndDate, request.SessionTemplates,
                request.PhysicianDataId);
            return response;
        }

        private static IEnumerable<DateOnly> EnumerateDates(DateOnly start, DateOnly end, List<DayOfWeek> daysOfWeek)
        {
            for (var date = start; date <= end; date = date.AddDays(1))
            {
                if (daysOfWeek.Contains(date.DayOfWeek))
                {
                    yield return date;
                }
            }
        }

        private static List<SessionTemplateDay> CreateSessionDays(
            Dictionary<DayOfWeek, DaySessionsDto> sessionTemplates,
            Guid timetableTemplateId)
        {
            var sessionDayTemplates = sessionTemplates.Select(pair => new SessionTemplateDay
            {
                DayOfWeek = pair.Key,
                IsActive = pair.Value.IsActive,
                TimetableTemplateId = timetableTemplateId,
            }).ToList();

            return sessionDayTemplates;
        }

        private static List<SessionTemplateTimes> CreateSessionTemplateTimes
        (
            List<SessionTemplateDay> sessionTemplateDays, 
            Dictionary<DayOfWeek, DaySessionsDto> sessionTemplates
        )
        {
            var createdSessionTemplates = new List<SessionTemplateTimes>();

            foreach (var dayTemplate in sessionTemplateDays)
            {
                var sessionTimes = sessionTemplates[dayTemplate.DayOfWeek].SessionTimeTemplates;
                dayTemplate.SessionTemplateTimes = sessionTimes.Select(st => new SessionTemplateTimes
                {
                    StartTime = TimeZoneInfo.ConvertTimeToUtc(st.StartTime),
                    EndTime = TimeZoneInfo.ConvertTimeToUtc(st.EndTime),
                    SessionTemplateDayId = dayTemplate.Id
                }).ToList();

                createdSessionTemplates.AddRange(dayTemplate.SessionTemplateTimes);
            }

            return createdSessionTemplates;
        }

        private async Task<Dictionary<DayOfWeek, DaySessionsDto>> GetTemplatesDictionaryAsync(
            Guid timetableTemplateId, CancellationToken cancellationToken)
        {
            var sessionDays = await _dbContext.SessionTemplateDays
                .Include(s => s.SessionTemplateTimes)
                .Where(s => s.TimetableTemplateId == timetableTemplateId)
                .ToListAsync(cancellationToken);

            sessionDays.ForEach(sd => sd.SessionTemplateTimes = sd.SessionTemplateTimes.OrderBy(st => st.StartTime).ToList());

            var weeklyTemplates = sessionDays
                .GroupBy(s => s.DayOfWeek)
                .ToDictionary(
                    group => group.Key,
                    group => new DaySessionsDto(
                        group.First().IsActive,
                        group.SelectMany(s => s.SessionTemplateTimes)
                            .Select(st => new SessionTimeTemplateDto(st.StartTime, st.EndTime))
                            .ToList()
                        ));

            return weeklyTemplates;
        }

        private static bool IsSessionTemplateMatch(
            Dictionary<DayOfWeek, DaySessionsDto> sessionTemplates, Session session)
        {
            if (!sessionTemplates.TryGetValue(session.StartTime.DayOfWeek, out var templates))
            {
                return false;
            }

            var sessionStartTime = TimeOnly.FromDateTime(session.StartTime);
            var sessionEndTime = TimeOnly.FromDateTime(session.EndTime);

            return templates.SessionTimeTemplates.Any(template =>
                TimeOnly.FromDateTime(template.StartTime) == sessionStartTime &&
                TimeOnly.FromDateTime(template.EndTime) == sessionEndTime);
        }

        private static List<SessionTimeTemplateDto> GetAvailableSessionsForDay(DateOnly day, IEnumerable<SessionTemplateDay> sessionDayTemplates, List<Session> assignedSessions)
        {
            var availableSessions = new List<SessionTimeTemplateDto>();
            var sessionTimesTemplates = sessionDayTemplates.Single(t => t.DayOfWeek == day.DayOfWeek).SessionTemplateTimes.ToList();

            foreach (var sessionTimeTemplate in sessionTimesTemplates)
            {
                var templateStartTime = new DateTime(day, TimeOnly.FromDateTime(sessionTimeTemplate.StartTime), DateTimeKind.Utc);
                var templateEndTime = new DateTime(day, TimeOnly.FromDateTime(sessionTimeTemplate.EndTime), DateTimeKind.Utc);

                var isAvailable = !assignedSessions.Any(s => s.StartTime < templateEndTime && templateStartTime < s.EndTime);

                if (isAvailable)
                {
                    availableSessions.Add(new SessionTimeTemplateDto(templateStartTime, templateEndTime));
                }
            }

            return availableSessions;
        }

        private async Task<List<SessionTemplateDay>> GetActiveSessionDayTemplatesAsync(Guid timetableTemplateId, CancellationToken cancellationToken)
        {
            var sessionDays = await _dbContext.SessionTemplateDays
                .Include(s => s.SessionTemplateTimes)
                .Where(s => s.TimetableTemplateId == timetableTemplateId && s.IsActive)
                .ToListAsync(cancellationToken);

            sessionDays.ForEach(sd => sd.SessionTemplateTimes = sd.SessionTemplateTimes.OrderBy(st => st.StartTime).ToList());

            return sessionDays;
        }

        private async Task<PhysicianInfoDto> GetPhysicianInfoAsync(Guid physicianId,
            CancellationToken cancellationToken)
        {
            var physician = await _dbContext.PhysicianData
                .Include(p => p.User)
                .ThenInclude(u => u.UserPhotoData)
                .FirstOrDefaultAsync(p => p.UserId == physicianId, cancellationToken);

            var specialties = await _dbContext.PhysicianSpecialties
                .Where(ps => ps.PhysicianDataId == physicianId)
                .Include(ps => ps.Position)
                .Select(ps => new PositionDto(ps.Position.Id, ps.Position.Specialty))
                .ToListAsync(cancellationToken);

            var result = new PhysicianInfoDto(physician.User.FirstName, physician.User.LastName,
                physician.User.Patronymic, physician.User.UserPhotoData?.PresignedUrl ?? "", specialties);

            return result;
        }

        private List<SessionTimeTemplateDto> GetDefaultSessions()
        {
            var sessionTemplates = new List<SessionTimeTemplateDto>();

            for (int i = 0; i < 5; i++)
            {
                var startTime = TimeZoneInfo.ConvertTimeToUtc(new DateTime(_defaultDateOnly, new TimeOnly(8 + i, 0)));
                var endTime = TimeZoneInfo.ConvertTimeToUtc(new DateTime(_defaultDateOnly, new TimeOnly(9 + i, 0)));

                sessionTemplates.Add(new SessionTimeTemplateDto(startTime, endTime));
            }

            return sessionTemplates;
        }

        private static Dictionary<DayOfWeek, DaySessionsDto> GetDefaultTimetableTemplate(DaySessionsDto daySessions)
        {
            return new Dictionary<DayOfWeek, DaySessionsDto>
            {
                { DayOfWeek.Monday, daySessions },
                { DayOfWeek.Tuesday, daySessions },
                { DayOfWeek.Wednesday, daySessions },
                { DayOfWeek.Thursday, daySessions },
                { DayOfWeek.Friday, daySessions }
            };
        }
    }
}
