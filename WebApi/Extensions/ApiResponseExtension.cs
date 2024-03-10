namespace WebApi.Extensions
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; } = default!;
    }

    public static class ApiResponseExtensions
    {
        public static ApiResponse<T> ToApiResponse<T>(this T data, bool isSuccess = true)
        {
            return new ApiResponse<T>
            {
                IsSuccess = isSuccess,
                Data = data
            };
        }
    }
}
