namespace Application.Physician.DTOs.ResponseDTOs
{
    public class GetPaginatedPhysiciansDto<T>
    {
        public List<T> Physicians { get; set; } = null!;
        public long TotalCount { get; set; }
    }
}
