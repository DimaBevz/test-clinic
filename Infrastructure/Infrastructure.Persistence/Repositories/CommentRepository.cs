using Application.Comment.DTOs;
using Application.Comment.DTOs.RequestDTOs;
using Application.Comment.DTOs.ResponseDTOs;
using Application.Common.Enums;
using Application.Common.Interfaces.Repositories;
using Infrastructure.Persistence.Mappers;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

internal class CommentRepository : ICommentRepository
{
    private readonly ApplicationDbContext _dbContext;
    
    public CommentRepository(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<CreateCommentResponseDto?> CreateCommentAsync(
        CreateCommentRequestDto createCommentRequestDto, 
        Guid patientId, 
        CancellationToken cancellationToken)
    {
        
        var commentToAdd = createCommentRequestDto.ToComment();
        commentToAdd.PatientDataId = patientId;
        
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        
        await _dbContext.Comments.AddAsync(commentToAdd, cancellationToken);
        var rowsAffected = await _dbContext.SaveChangesAsync(cancellationToken);
        
        var isUpdateRankSuccess = await UpdatePhysicianRank(createCommentRequestDto.PhysicianId, cancellationToken);
        
        var result = rowsAffected > 0 && isUpdateRankSuccess;
        if (result)
        {
            await transaction.CommitAsync(cancellationToken);
            var commentResponse = await _dbContext.Comments
                .Where(x => x.Id == commentToAdd.Id)
                .Include(x => x.PatientData)
                .ThenInclude(x => x.User)
                .Include(x => x.PhysicianData)
                .ThenInclude(x => x.User)
                .FirstAsync(cancellationToken);

            return commentResponse.ToCommentCreated();
        }

        await transaction.RollbackAsync(cancellationToken);
        return default;
    }

    public async Task<List<GetCommentResponseDto>> GetCommentsAsync(Guid physicianDataId, CancellationToken cancellationToken)
    {
        var comments = await _dbContext.Comments
            .Include(x=>x.PatientData)
            .ThenInclude(x=>x.User)
            .Include(x=>x.PhysicianData)
            .ThenInclude(x=>x.User)
            .Where(x => x.PhysicianDataId == physicianDataId)
            .ToListAsync(cancellationToken);

        var response = comments.Select(x => x.ToGetComment()).ToList();
        return response;
    }

    public async Task<bool> DeleteCommentAsync(
        Guid id,
        Guid currentUserId,
        string role,
        CancellationToken cancellationToken)
    {
        var commentToDelete = await _dbContext.Comments
            .FirstAsync(x => x.Id == id, cancellationToken: cancellationToken);
        
        if (commentToDelete.PatientDataId != currentUserId &&
            role != nameof(RoleType.Admin)) return false;
        
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        
        _dbContext.Comments.Remove(commentToDelete);
        var rowsAffected = await _dbContext.SaveChangesAsync(cancellationToken);

        var isUpdateRankSuccess = await UpdatePhysicianRank(commentToDelete.PhysicianDataId, cancellationToken);
        var result = rowsAffected > 0 && isUpdateRankSuccess;
        if (result)
        {
            await transaction.CommitAsync(cancellationToken);
            return result;
        }

        await transaction.RollbackAsync(cancellationToken);
        return result;
    }

    public async Task<UpdateCommentResponseDto?> UpdateCommentAsync(
        UpdateCommentRequestDto updateCommentRequestDto,
        Guid currentUserId, 
        CancellationToken cancellationToken)
    {
        var commentToUpdate = await _dbContext.Comments
            .FirstAsync(x => x.Id == updateCommentRequestDto.Id, cancellationToken);
        if (currentUserId != commentToUpdate.PatientDataId) return default;
        
        await using var transaction = await _dbContext.Database.BeginTransactionAsync(cancellationToken);
        
        CommentMapper.ToCommentUpdate(updateCommentRequestDto,commentToUpdate);
        _dbContext.Comments.Update(commentToUpdate);
        var rowsAffected = await _dbContext.SaveChangesAsync(cancellationToken);
        
        var isUpdateRankSuccess = await UpdatePhysicianRank(commentToUpdate.PhysicianDataId, cancellationToken);
        
        var result = rowsAffected > 0 && isUpdateRankSuccess;
        if (result)
        {
            await transaction.CommitAsync(cancellationToken);
            var commentResponse = await _dbContext.Comments
                .Where(x => x.Id == commentToUpdate.Id)
                .Include(x => x.PatientData)
                .ThenInclude(x => x.User)
                .Include(x => x.PhysicianData)
                .ThenInclude(x => x.User)
                .FirstAsync(cancellationToken);

            return commentResponse.ToCommentUpdated();
        }

        await transaction.RollbackAsync(cancellationToken);
        return  default;
    }

    private async Task<bool> UpdatePhysicianRank(Guid physicianDataId, CancellationToken cancellationToken)
    {
        var physician = await _dbContext.PhysicianData
            .SingleAsync(x => x.UserId == physicianDataId, 
                cancellationToken);
        
        var rating = await _dbContext.Comments
            .Where(x => x.PhysicianDataId == physicianDataId)
            .AverageAsync(x=>x.Rating, cancellationToken);
        physician.Rating = (float)Math.Round(rating * 2, MidpointRounding.AwayFromZero) / 2;

        _dbContext.PhysicianData.Update(physician);
        var rowsAffected = await _dbContext.SaveChangesAsync(cancellationToken);
        var result = rowsAffected > 0;
        return result;
    }

}