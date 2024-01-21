using Application.Comment.DTOs.RequestDTOs;
using Application.Comment.DTOs.ResponseDTOs;
using Infrastructure.Persistence.Entities;
using Riok.Mapperly.Abstractions;

namespace Infrastructure.Persistence.Mappers;

[Mapper]
internal static partial class CommentMapper
{
    [MapProperty(nameof(@CreateCommentRequestDto.PhysicianId),nameof(Comment.PhysicianDataId))]
    public static partial Comment ToComment(this CreateCommentRequestDto source);

    [MapProperty(nameof(@Comment.PatientData.User.FirstName),nameof(GetCommentResponseDto.FirstName))]
    [MapProperty(nameof(@Comment.PatientData.User.LastName),nameof(GetCommentResponseDto.LastName))]
    [MapProperty(nameof(@Comment.PatientData.User.Id),nameof(GetCommentResponseDto.AuthorId))]
    public static partial GetCommentResponseDto ToGetComment(this Comment source);

    [MapProperty(nameof(@Comment.PatientData.User.FirstName),nameof(CreateCommentResponseDto.FirstName))]
    [MapProperty(nameof(@Comment.PatientData.User.LastName),nameof(CreateCommentResponseDto.LastName))]
    [MapProperty(nameof(@Comment.PatientData.User.Id),nameof(CreateCommentResponseDto.AuthorId))]
    public static partial CreateCommentResponseDto ToCommentCreated(this Comment source);
    
    [MapProperty(nameof(@Comment.PatientData.User.FirstName),nameof(UpdateCommentResponseDto.FirstName))]
    [MapProperty(nameof(@Comment.PatientData.User.LastName),nameof(UpdateCommentResponseDto.LastName))]
    [MapProperty(nameof(@Comment.PatientData.User.Id),nameof(UpdateCommentResponseDto.AuthorId))]
    public static partial UpdateCommentResponseDto ToCommentUpdated(this Comment source);

    public static partial void ToCommentUpdate(UpdateCommentRequestDto source, Comment dest);
    
}