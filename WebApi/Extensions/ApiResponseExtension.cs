namespace WebApi.Extensions
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T Data { get; set; } = default!;
    }

    public static class ApiResponseExtensions
    {
        public static ApiResponse<T> ToApiResponse<T>(this T data, bool isSuccess = true, string? message = null)
        {
            return new ApiResponse<T>
            {
                IsSuccess = isSuccess,
                Message = message,
                Data = data
            };
        }
    }
}
