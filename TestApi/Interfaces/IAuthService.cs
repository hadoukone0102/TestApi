using TestApi.DTOs.Users;
using TestApi.Helpers;

namespace TestApi.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<string>> RegisterUser(UserDto userDto);
        Task<ApiResponse<string>> LoginUser(UserDto userDto);
    }
}
