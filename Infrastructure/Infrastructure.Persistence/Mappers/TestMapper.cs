using Application.Test.DTOs.ResponseDTOs;
using Infrastructure.Persistence.Entities;
using Riok.Mapperly.Abstractions;

namespace Infrastructure.Persistence.Mappers;

[Mapper]
internal static partial class TestMapper
{
    public static partial GetTestDto ToGetTestDto(this Test source);

    public static GetQuestionDto ToGetQuestionDto(this TestQuestion source)
    {
        var result = new GetQuestionDto(
            source.Id,
            source.Text,
            source.Number,
            source.TestOptions.Select(x => x.ToGetOptionDto()).ToList());
        
        return result;
    }

    public static partial GetOptionDto ToGetOptionDto(this TestOption source);
}