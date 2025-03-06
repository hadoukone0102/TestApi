using TestApi.DTOs.Users;

namespace TestApi.Helpers
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T Data { get; set; } = default!;
        public ApiResponse(bool success, string message,T Data) {
            this.Success = success;
            this.Message = message;
            this.Data = Data;
        }
    }
}
