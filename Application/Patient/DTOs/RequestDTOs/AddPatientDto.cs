namespace Application.Patient.DTOs.RequestDTOs
{
    public record AddPatientDto
    (
        Guid Id,
        int? Apartment = null,
        string Settlement = "",
        string Street = "",
        string House = "",
        string Institution = "",
        string Position = ""
    );
}
