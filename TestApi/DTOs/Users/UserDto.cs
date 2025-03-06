namespace TestApi.DTOs.Users
{
    public class UserDto
    {
        public string PasswordHash { get; set; }= string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
