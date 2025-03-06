using System.ComponentModel.DataAnnotations;

namespace TestApi.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        public byte[] PasswordSalt { get; set; } = new byte[0];
    }
}
