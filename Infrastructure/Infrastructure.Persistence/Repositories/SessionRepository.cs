using Application.Common.Enums;
using Application.Common.Interfaces.Repositories;
using Application.Session.DTOs.Request;
using Application.Session.DTOs.Response;
using Infrastructure.Persistence.Entities;
using Infrastructure.Persistence.Mappers;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace Infrastructure.Persistence.Repositories
{
    internal class SessionRepository : ISessionRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public SessionRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<AddedSessionDto> AddSessionAsync(AddSessionDto request, Guid patientId, Guid meetingId, CancellationToken cancellationToken)
        {
            var startTimeOnly = TimeOnly.FromDateTime(request.StartTime);
            var endTimeOnly = TimeOnly.FromDateTime(request.EndTime);

            var startTime = DateTime.SpecifyKind(request.SessionDate.ToDateTime(startTimeOnly), DateTimeKind.Utc);
            var endTime = DateTime.SpecifyKind(request.SessionDate.ToDateTime(endTimeOnly), DateTimeKind.Utc);

            if (await IsNewSessionIntersecting(request.PhysicianId, patientId, startTime, endTime, cancellationToken))
            {
                return null;
            }

            var sessionId = Guid.NewGuid();
            var session = new Session
            {
                Id = sessionId,
                StartTime = startTime,
                EndTime = endTime,
                MeetingId = meetingId,
                PhysicianDataId = request.PhysicianId,
                PatientDataId = patientId,
                MeetingHistory = new MeetingHistory
                {
                    SessionId = sessionId
                }
            };

            var details = new SessionDetail
            {
                SessionId = session.Id,
                CurrentPainScale = request.CurrentPainScale,
                AveragePainScaleLastMonth = request.AveragePainScaleLastMonth,
                HighestPainScaleLastMonth = request.HighestPainScaleLastMonth,
                Complaints = request.Description
            };

            await _dbContext.Sessions.AddAsync(session, cancellationToken);
            await _dbContext.SessionDetails.AddAsync(details, cancellationToken);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var sessionDetails = new AddedSessionDto(session.Id, session.StartTime, session.EndTime,
                session.PhysicianDataId, details.Complaints, details.CurrentPainScale,
                details.AveragePainScaleLastMonth, details.HighestPainScaleLastMonth);

            return sessionDetails;
        }

        public async Task<GetSessionsResponseDto> GetSessionsAsync(GetSessionsRequestDto request, CancellationToken cancellationToken)
        {
            if (!request.PhysicianId.HasValue && !request.PatientId.HasValue)
            {
                return null;
            }

            var sessionsInstances = GetQueryableSessions(request.PhysicianId, request.PatientId, request.StartTime,
                request.EndTime);

            switch (request.SortType)
            {
                case SessionSortType.Active:
                    sessionsInstances = sessionsInstances
                        .Where(s => !s.IsArchived && !s.IsDeleted);
                    break;
                case SessionSortType.All:
                    break;
                case SessionSortType.Archived:
                    sessionsInstances = sessionsInstances
                        .Where(s => s.IsArchived);
                    break;
                default:
                    sessionsInstances = sessionsInstances
                        .Where(s => !s.IsArchived && !s.IsDeleted);
                    break;
            }

            var sessions = await sessionsInstances.OrderBy(s => s.StartTime)
                .Select(s => CreateSessionItem(s))
                .ToListAsync(cancellationToken);

            var resultSessionList = new List<SessionItemDto>();

            var activeSessions = sessions.Where(s => !s.IsDeleted && !s.IsArchived);
            var archivedSessions = sessions.Where(s => s.IsArchived);

            resultSessionList.AddRange(activeSessions);
            resultSessionList.AddRange(archivedSessions);
            resultSessionList.AddRange(sessions
                .Except(activeSessions)
                .Except(archivedSessions));

            var response = new GetSessionsResponseDto(resultSessionList);
            return response;
        }

        public async Task<GetPaginatedSessionsDto> GetSessionsByParamsAsync(GetSessionsByParamsDto request, CancellationToken cancellationToken)
        {
            if (!request.PhysicianId.HasValue && !request.PatientId.HasValue)
            {
                return null;
            }

            var query = GetQueryableSessions(request.PhysicianId, request.PatientId, request.StartTime,
                request.EndTime);

            switch (request.SortType)
            {
                case SessionSortType.Active:
                    query = query
                        .Where(s => !s.IsArchived && !s.IsDeleted);
                    break;
                case SessionSortType.All:
                    break;
                case SessionSortType.Archived:
                    query = query
                        .Where(s => s.IsArchived);
                    break;
                default:
                    query = query
                        .Where(s => !s.IsArchived && !s.IsDeleted);
                    break;
            }

            var sessionCount = query.Count();

            var sessionItems = await query
                .OrderBy(s => s.StartTime)
                .Skip(request.Limit * (request.Page - 1))
                .Take(request.Limit)
                .Select(s => CreateSessionItem(s))
                .ToListAsync(cancellationToken);

            var result = new GetPaginatedSessionsDto(sessionItems, sessionCount);

            return result;
        }

        public async Task<SessionDetailsDto> GetSessionByIdAsync(Guid sessionId, CancellationToken cancellationToken)
        {
            var session = await _dbContext.Sessions
                .Include(s => s.SessionDetail)
                .ThenInclude(sd => sd.Diagnosis)
                .Include(s => s.PatientData)
                .ThenInclude(p => p.User)
                .ThenInclude(u => u.UserPhotoData)
                .Include(s=>s.PhysicianData)
                .ThenInclude(p => p.Positions)
                .Include(s => s.PhysicianData)
                .ThenInclude(p => p.User)
                .ThenInclude(u => u.UserPhotoData)
                .FirstOrDefaultAsync(s => s.Id == sessionId, cancellationToken);

            if (session is null)
            {
                return null;
            }

            var result = await GetSessionDetailsDto(session, session.SessionDetail, session.SessionDetail.Diagnosis,
                cancellationToken);

            return result;
        }

        public async Task<Guid?> GetMeetingIdAsync(Guid sessionId, CancellationToken cancellationToken)
        {
            var session = await _dbContext.Sessions.FirstOrDefaultAsync(s => s.Id == sessionId, cancellationToken);

            return session?.MeetingId;
        }

        public async Task<SessionDetailsDto> UpdateSessionAsync(UpdateSessionRequestDto request, CancellationToken cancellationToken)
        {
            var session = request.ToSession();

            if (session.Id == Guid.Empty)
            {
                return null;
            }

            var sessionDetails = request.ToUpdatedSessionDetails();
            var diagnosis = request.ToDiagnosis();

            var diagnosisInstance = await _dbContext.SessionDetails
                .Include(d => d.Diagnosis)
                .FirstOrDefaultAsync(d => d.SessionId == sessionDetails.SessionId, cancellationToken);
            var diagnosisId = diagnosisInstance?.DiagnosisId;

            var diagnosisUpdate = await UpdateDiagnosisAsync(diagnosis, diagnosisId, diagnosisInstance?.Diagnosis, cancellationToken);

            var updatedDetails = await UpdateSessionDetailsAsync(sessionDetails, diagnosisUpdate, cancellationToken);

            var updateResult = await UpdateSessionAsync(session, cancellationToken);
            if (updateResult is null)
            {
                return null;
            }

            await _dbContext.SaveChangesAsync(cancellationToken);

            var result = await GetSessionDetailsDto(updateResult, updatedDetails, diagnosis, cancellationToken);

            return result;
        }

        public async Task<SessionItemDto> UpdateArchiveStatusAsync(Guid sessionId, CancellationToken cancellationToken)
        {
            var session = await _dbContext.Sessions
                .Include(s => s.SessionDetail)
                .Include(s => s.PatientData)
                .ThenInclude(p => p.User)
                .Include(s => s.PhysicianData)
                .ThenInclude(p => p.User)
                .FirstOrDefaultAsync(s => s.Id == sessionId, cancellationToken);

            if (session is null)
            {
                return null;
            }

            session.IsArchived = !session.IsArchived;
            _dbContext.Sessions.Update(session);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var result = CreateSessionItem(session);
            return result;
        }

        public async Task<SessionItemDto> DeleteSessionAsync(Guid sessionId, CancellationToken cancellationToken)
        {
            var session = await _dbContext.Sessions
                .Include(s => s.SessionDetail)
                .Include(s => s.PatientData)
                .ThenInclude(p => p.User)
                .Include(s => s.PhysicianData)
                .ThenInclude(p => p.User)
                .FirstOrDefaultAsync(s => s.Id == sessionId, cancellationToken);

            if (session is null)
            {
                return null;
            }

            session.IsDeleted = true;
            _dbContext.Sessions.Update(session);
            await _dbContext.SaveChangesAsync(cancellationToken);

            var result = CreateSessionItem(session);
            return result;
        }

        private async Task<bool> IsIntersecting(Guid sessionId, Guid physicianId, DateTime startTime, DateTime endTime, CancellationToken cancellationToken)
        {
            var isIntersecting = await _dbContext.Sessions
                .AnyAsync(s => s.Id != sessionId &&
                               s.PhysicianDataId == physicianId &&
                               s.StartTime < endTime &&
                               s.EndTime > startTime, cancellationToken);

            return isIntersecting;
        }

        private async Task<bool> IsNewSessionIntersecting(Guid physicianId, Guid patientId, DateTime startTime, DateTime endTime, CancellationToken cancellationToken)
        {
            var sessionsIntersect = await _dbContext.Sessions
                .AnyAsync(s => (s.PhysicianDataId == physicianId ||
                                                s.PatientDataId == patientId) &&
                                                s.StartTime < endTime &&
                                                s.EndTime > startTime, cancellationToken);

            return sessionsIntersect;
        }

        private async Task<Session> UpdateSessionAsync(Session sessionUpdate, CancellationToken cancellationToken)
        {
            var session = await _dbContext.Sessions
                .Include(s => s.SessionDetail)
                .ThenInclude(sd => sd != null ? sd.Diagnosis : null)
                .Include(s => s.PatientData)
                .ThenInclude(p => p.User)
                .ThenInclude(u => u.UserPhotoData)
                .Include(s => s.PhysicianData)
                .ThenInclude(p => p.Positions)
                .Include(s => s.PhysicianData)
                .ThenInclude(p => p.User)
                .ThenInclude(u => u.UserPhotoData)
                .FirstOrDefaultAsync(s => s.Id == sessionUpdate.Id, cancellationToken);

            session.PatientDataId = sessionUpdate.PatientDataId == Guid.Empty ? session.PatientDataId : sessionUpdate.PatientDataId;

            if (sessionUpdate.StartTime != default && sessionUpdate.EndTime != default)
            {
                if (!await IsIntersecting(session.Id, session.PhysicianDataId, sessionUpdate.StartTime, sessionUpdate.EndTime, cancellationToken))
                {
                    session.StartTime = sessionUpdate.StartTime;
                    session.EndTime = sessionUpdate.EndTime;
                }
                else
                {
                    return null;
                }
            }

            _dbContext.Sessions.Update(session);
            return session;
        }

        private async Task<SessionDetail> UpdateSessionDetailsAsync(SessionDetail detailsUpdate, Guid? diagnosisId, CancellationToken cancellationToken)
        {
            var sessionDetails =
                await _dbContext.SessionDetails
                    .FirstOrDefaultAsync(d => d.SessionId == detailsUpdate.SessionId, cancellationToken);

            if (sessionDetails is not null)
            {
                UpdateSessionDetailsIfNotEmpty(ref sessionDetails, detailsUpdate, diagnosisId);
                _dbContext.SessionDetails.Update(sessionDetails);
                return sessionDetails;
            }

            if (AreAllFieldsNotEmpty(detailsUpdate))
            {
                var updatedDetails = new SessionDetail
                {
                    SessionId = detailsUpdate.SessionId,
                    Complaints = detailsUpdate.Complaints,
                    Treatment = detailsUpdate.Treatment,
                    Recommendations = detailsUpdate.Recommendations,
                    DiagnosisId = diagnosisId ?? sessionDetails.DiagnosisId
                };
                await _dbContext.SessionDetails.AddAsync(updatedDetails, cancellationToken);
                return updatedDetails;
            }

            return null;
        }

        void UpdateSessionDetailsIfNotEmpty(ref SessionDetail sessionDetails, SessionDetail detailsUpdate, Guid? diagnosisId)
        {
            sessionDetails.Complaints = string.IsNullOrEmpty(detailsUpdate.Complaints) ? sessionDetails.Complaints : detailsUpdate.Complaints;
            sessionDetails.Treatment = string.IsNullOrEmpty(detailsUpdate.Treatment) ? sessionDetails.Treatment : detailsUpdate.Treatment;
            sessionDetails.Recommendations = string.IsNullOrEmpty(detailsUpdate.Recommendations) ? sessionDetails.Recommendations : detailsUpdate.Recommendations;
            sessionDetails.DiagnosisId = diagnosisId ?? sessionDetails.DiagnosisId;
        }

        private bool AreAllFieldsNotEmpty(SessionDetail detailsUpdate)
        {
            return !string.IsNullOrEmpty(detailsUpdate.Complaints)
                   && !string.IsNullOrEmpty(detailsUpdate.Treatment)
                   && !string.IsNullOrEmpty(detailsUpdate.Recommendations);
        }

        private async Task<Guid?> UpdateDiagnosisAsync(Diagnosis diagnosis, Guid? diagnosisId, Diagnosis? diagnosisSource, CancellationToken cancellationToken)
        {
            if (!string.IsNullOrEmpty(diagnosis.Title))
            {
                if (diagnosisId.HasValue)
                {
                    diagnosis.Id = diagnosisId.Value;
                    diagnosisSource.Title = diagnosis.Title;
                    _dbContext.Diagnoses.Update(diagnosisSource);
                    return diagnosis.Id;
                }
                diagnosis.Id = Guid.NewGuid();
                await _dbContext.Diagnoses.AddAsync(diagnosis, cancellationToken);
                return diagnosis.Id;
            }

            if (diagnosisId.HasValue)
            {
                diagnosis.Title = diagnosisSource.Title;
            }

            return diagnosisId;
        }

        private async Task<List<DocumentItemDto>> GetDocumentItemsAsync(Guid sessionId, CancellationToken cancellationToken)
        {
            return await _dbContext.Documents.Where(d => d.SessionDetailSessionId == sessionId)
                .Select(d => new DocumentItemDto(d.Title, d.PresignedUrl))
                .ToListAsync(cancellationToken);
        }

        private async Task<SessionDetailsDto> GetSessionDetailsDto(Session session, SessionDetail sessionDetail,
            Diagnosis diagnosis, CancellationToken cancellationToken)
        {
            var physicianPhoto = await _dbContext.UserPhotoData
                .Where(p => p.UserId == session.PhysicianDataId)
                .Select(p => p.PresignedUrl)
                .FirstOrDefaultAsync(cancellationToken);

            var physicianSpecialties = session.PhysicianData.Positions?.Select(p => p.ToDto()).ToList();
            var physician = new PhysicianDataDto(session.PhysicianData.User.Id, session.PhysicianData.User.LastName,
                session.PhysicianData.User.FirstName, session.PhysicianData.User.Patronymic,
                physicianPhoto ?? "", session.PhysicianData.Bio,
                physicianSpecialties);

            var patientPhoto = await _dbContext.UserPhotoData
                .Where(p => p.UserId == session.PatientDataId)
                .Select(p => p.PresignedUrl)
                .FirstOrDefaultAsync(cancellationToken);

            var patient = new PatientDataDto(session.PatientData.User.Id, session.PatientData.User.LastName,
                session.PatientData.User.FirstName, session.PatientData.User.Patronymic,
                patientPhoto ?? "");

            var documents = await GetDocumentItemsAsync(session.Id, cancellationToken);

            var result = new SessionDetailsDto(
                session.Id, session.StartTime, session.EndTime, physician,
                patient, sessionDetail.CurrentPainScale,
                sessionDetail.AveragePainScaleLastMonth, sessionDetail.HighestPainScaleLastMonth,
                sessionDetail.Complaints, sessionDetail.Treatment,
                sessionDetail.Recommendations, diagnosis?.Title, session.MeetingId, session.IsArchived,
                session.IsDeleted, documents);

            return result;
        }

        private IQueryable<Session> GetQueryableSessions(Guid? physicianId, Guid? patientId, DateTime? startTime, DateTime? endTime)
        {
            return _dbContext.Sessions
                .Include(s => s.PatientData)
                .ThenInclude(ps => ps.User)
                .Include(s => s.PhysicianData)
                .ThenInclude(p => p.User)
                .Include(s => s.SessionDetail)
                .ThenInclude(sd => sd != null ? sd.Diagnosis : null)
                .Where(s =>
                    (!physicianId.HasValue || s.PhysicianDataId == physicianId.Value) &&
                    (!patientId.HasValue || s.PatientDataId == patientId.Value) &&
                    (!startTime.HasValue || s.StartTime >= startTime.Value.Date) &&
                    (!endTime.HasValue || s.EndTime <= endTime.Value.Date.AddDays(1).AddTicks(-1))
                )
                .AsQueryable();
        }

        private static SessionItemDto CreateSessionItem(Session session)
        {
            var physician = new UserInfoDto(session.PhysicianData.User.Id, session.PhysicianData.User.LastName,
                session.PhysicianData.User.FirstName, session.PhysicianData.User.Patronymic);
            var patient = new UserInfoDto(session.PatientData.User.Id, session.PatientData.User.LastName,
                session.PatientData.User.FirstName, session.PatientData.User.Patronymic);

            return new SessionItemDto(session.Id, session.StartTime, session.EndTime, physician, patient,
                session.IsArchived, session.IsDeleted, session.SessionDetail.CurrentPainScale,
                session.SessionDetail.AveragePainScaleLastMonth, session.SessionDetail.HighestPainScaleLastMonth);
        }
    }
}
