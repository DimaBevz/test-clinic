using Application.Military.DTOs.Response;

namespace Application.Patient.DTOs.ResponseDTOs
{
    public record GetPatientDataDto
    (
        Guid Id,
        string Settlement,
        string Street,
        string House,
        int? Apartment,
        string Institution,
        string Position,
        GetMilitaryDataDto? MilitaryData
    );
}
