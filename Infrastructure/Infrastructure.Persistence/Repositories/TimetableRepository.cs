using Application.Common.Interfaces.Repositories;
using Application.TimetableTemplate.DTOs;
using Application.TimetableTemplate.DTOs.Request;
using Application.TimetableTemplate.DTOs.Response;
using Infrastructure.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

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
            var startDate = DateOnly.FromDateTime(DateTime.Now);
            var endDate = DateOnly.FromDateTime(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day+7));
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

        public async Task<GetAvailableTimetableDto> GetAvailableTimetableAsync(Guid physicianId, CancellationToken cancellationToken)
        {
            var timetableTemplate = await _dbContext.TimetableTemplates
                .FirstOrDefaultAsync(t => t.PhysicianDataId == physicianId, cancellationToken);

            if (timetableTemplate is null)
            {
                return null;
            }

            var sessionDays = await GetSessionDayTemplatesAsync(timetableTemplate.Id, cancellationToken);
            var sessions = await _dbContext.Sessions
                .Where(s => s.PhysicianDataId == physicianId)
                .ToListAsync(cancellationToken);

            var availableTimetable = new Dictionary<DateOnly, List<SessionTimeTemplateDto>>();

            var timetableDates = EnumerateDates(timetableTemplate.StartDate, timetableTemplate.EndDate,
                sessionDays.Select(st => st.DayOfWeek).Distinct().ToList())
                .Where(d => d.ToDateTime(TimeOnly.MinValue) >= DateTime.Today)
                .ToList();

            foreach (var day in timetableDates)
            {
                var availableSessionsForDay = GetAvailableSessionsForDay(day, sessionDays, sessions);
                if (availableSessionsForDay.Any())
                {
                    availableTimetable.Add(day, availableSessionsForDay);
                }
            }

            var physician = await GetPhysicianInfoAsync(physicianId, cancellationToken);

            var result = new GetAvailableTimetableDto(availableTimetable, physician);

            return result;
        }

        public async Task<UpdateTimetableTemplateDto> UpdateTemplateAsync(UpdateTimetableTemplateDto request, CancellationToken cancellationToken)
        {
            var timetableTemplate = await _dbContext.TimetableTemplates
                    .FirstOrDefaultAsync(t => t.PhysicianDataId == request.PhysicianDataId,
                    cancellationToken);

            var sessionDays = await _dbContext.SessionDayTemplates
                .Include(s => s.SessionTemplates)
                .Where(s => s.TimetableTemplateId == timetableTemplate.Id)
                .ToListAsync(cancellationToken);

            _dbContext.SessionDayTemplates.RemoveRange(sessionDays);
            timetableTemplate.StartDate = request.StartDate;
            timetableTemplate.EndDate = request.EndDate;
            _dbContext.TimetableTemplates.Update(timetableTemplate);

            var sessionDaysUpdate = CreateSessionDays(request.SessionTemplates, timetableTemplate.Id);

            var sessionTemplatesUpdate =
                CreateSessionTemplates(sessionDaysUpdate, request.SessionTemplates);

            await _dbContext.SessionDayTemplates.AddRangeAsync(sessionDaysUpdate, cancellationToken);
            await _dbContext.SessionTemplates.AddRangeAsync(sessionTemplatesUpdate, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var result = new UpdateTimetableTemplateDto(request.StartDate, request.EndDate, request.SessionTemplates,
                request.PhysicianDataId);
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

            var sessionTemplates =
                CreateSessionTemplates(sessionDays, request.SessionTemplates);

            await _dbContext.SessionDayTemplates.AddRangeAsync(sessionDays, cancellationToken);
            await _dbContext.SessionTemplates.AddRangeAsync(sessionTemplates, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var response = new AddTimetableTemplateDto(request.StartDate, request.EndDate, request.SessionTemplates,
                request.PhysicianDataId);
            return response;
        }

        private IEnumerable<DateOnly> EnumerateDates(DateOnly start, DateOnly end, List<DayOfWeek> daysOfWeek)
        {
            for (var date = start; date <= end; date = date.AddDays(1))
            {
                if (daysOfWeek.Contains(date.DayOfWeek))
                {
                    yield return date;
                }
            }
        }

        private List<SessionDayTemplate> CreateSessionDays(Dictionary<DayOfWeek, DaySessionsDto> sessionTemplates,
            Guid timetableTemplateId)
        {
            var sessionDayTemplates = sessionTemplates.Select(pair => new SessionDayTemplate
            {
                Id = Guid.NewGuid(),
                DayOfWeek = pair.Key,
                IsActive = pair.Value.IsActive,
                TimetableTemplateId = timetableTemplateId,
            }).ToList();

            return sessionDayTemplates;
        }

        private List<SessionTemplate> CreateSessionTemplates(
            List<SessionDayTemplate> sessionDayTemplates, Dictionary<DayOfWeek, DaySessionsDto> sessionTemplates)
        {
            var createdSessionTemplates = new List<SessionTemplate>();

            foreach (var dayTemplate in sessionDayTemplates)
            {
                var sessionTimes = sessionTemplates[dayTemplate.DayOfWeek].SessionTimeTemplates;
                dayTemplate.SessionTemplates = sessionTimes.Select(st => new SessionTemplate
                {
                    Id = Guid.NewGuid(),
                    StartTime = TimeOnly.FromDateTime(st.StartTime),
                    EndTime = TimeOnly.FromDateTime(st.EndTime),
                    SessionDayTemplateId = dayTemplate.Id
                }).ToList();
                createdSessionTemplates.AddRange(dayTemplate.SessionTemplates);
            }

            return createdSessionTemplates;
        }

        private async Task<Dictionary<DayOfWeek, DaySessionsDto>> GetTemplatesDictionaryAsync(
            Guid timetableTemplateId, CancellationToken cancellationToken)
        {
            var sessionDays = await _dbContext.SessionDayTemplates
                .Include(s => s.SessionTemplates)
                .Where(s => s.TimetableTemplateId == timetableTemplateId)
                .ToListAsync(cancellationToken);

            sessionDays.ForEach(sd => sd.SessionTemplates = sd.SessionTemplates.OrderBy(st => st.StartTime).ToList());

            var weeklyTemplates = sessionDays
                .GroupBy(s => s.DayOfWeek)
                .ToDictionary(
                    group => group.Key,
                    group => new DaySessionsDto(
                        group.First().IsActive,
                        group.SelectMany(s => s.SessionTemplates)
                            .Select(st => new SessionTimeTemplateDto(
                                new DateTime(_defaultDateOnly, st.StartTime),
                                new DateTime(_defaultDateOnly, st.EndTime)))
                            .ToList()
                        ));

            return weeklyTemplates;
        }

        private bool IsSessionTemplateMatch(
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

        private List<SessionTimeTemplateDto> GetAvailableSessionsForDay(DateOnly day, IEnumerable<SessionDayTemplate> sessionDayTemplates, List<Session> sessions)
        {
            var availableSessions = new List<SessionTimeTemplateDto>();

            var sessionTemplates = sessionDayTemplates.FirstOrDefault(t => t.DayOfWeek == day.DayOfWeek);

            foreach (var template in sessionTemplates.SessionTemplates)
            {
                if (DateTime.Now < day.ToDateTime(template.StartTime))
                {
                    var startTime = DateTime.SpecifyKind(day.ToDateTime(template.StartTime), DateTimeKind.Utc);
                    var endTime = DateTime.SpecifyKind(day.ToDateTime(template.EndTime), DateTimeKind.Utc);

                    var isAvailable = !sessions.Any(s => s.StartTime < endTime && startTime < s.EndTime);

                    if (isAvailable)
                    {
                        availableSessions.Add(new SessionTimeTemplateDto(new DateTime(_defaultDateOnly, template.StartTime), new DateTime(_defaultDateOnly, template.EndTime)));
                    }
                }
            }

            return availableSessions;
        }

        private async Task<List<SessionDayTemplate>> GetSessionDayTemplatesAsync(Guid timetableTemplateId, CancellationToken cancellationToken)
        {
            var sessionDays = await _dbContext.SessionDayTemplates
                .Include(s => s.SessionTemplates)
                .Where(s => s.TimetableTemplateId == timetableTemplateId && s.IsActive)
                .ToListAsync(cancellationToken);

            sessionDays.ForEach(sd => sd.SessionTemplates = sd.SessionTemplates.OrderBy(st => st.StartTime).ToList());

            return sessionDays;
        }

        private async Task<PhysicianInfoDto> GetPhysicianInfoAsync(Guid physicianId,
            CancellationToken cancellationToken)
        {
            var user = await _dbContext.Users
                .FirstOrDefaultAsync(u => u.Id==physicianId, cancellationToken);

            var userPhoto = await _dbContext.UserPhotoData
                .Where(p => p.UserId == physicianId)
                .Select(p => p.PresignedUrl)
                .FirstOrDefaultAsync(cancellationToken);

            var result = new PhysicianInfoDto(user.FirstName, user.LastName, user.Patronymic, userPhoto ?? "");

            return result;
        }

        private List<SessionTimeTemplateDto> GetDefaultSessions()
        {
            var sessionTemplates = new List<SessionTimeTemplateDto>();
            for (int i = 0; i < 5; i++)
            {
                sessionTemplates.Add(new SessionTimeTemplateDto(
                    new DateTime(_defaultDateOnly, new TimeOnly(8 + i, 0)),
                    new DateTime(_defaultDateOnly, new TimeOnly(9 + i, 0))));
            }
            return sessionTemplates;
        }

        private Dictionary<DayOfWeek, DaySessionsDto> GetDefaultTimetableTemplate(DaySessionsDto daySessions)
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
