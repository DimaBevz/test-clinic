namespace Application.Patient.DTOs.RequestDTOs
{
    public record UpdatePatientDto
    (
        Guid Id,
        string Settlement,
        string Street,
        string House,
        int? Apartment,
        string Institution,
        string Position
    );
}
