using Application.Chat.DTOs.RequestDTOs;
using Application.Chat.DTOs.ResponseDTOs;
using Infrastructure.Persistence.Entities;
using Riok.Mapperly.Abstractions;

namespace Infrastructure.Persistence.Mappers;

[Mapper]
internal static partial class ChatMapper
{

    [MapProperty(nameof(@ChatHistory.User.FirstName), nameof(MessageItemDto.FirstName))]
    [MapProperty(nameof(@ChatHistory.User.LastName), nameof(MessageItemDto.LastName))]
    [MapProperty(nameof(@ChatHistory.User.Patronymic), nameof(MessageItemDto.Patronymic))]
    public static partial MessageItemDto ToMessageItemDto(this ChatHistory source);

    public static partial ChatHistory ToEntity(this AddMessageDto source);
}
