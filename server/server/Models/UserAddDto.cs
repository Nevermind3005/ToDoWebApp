using System.ComponentModel.DataAnnotations;

namespace server.Models;

public class UserAddDto
{
    [Required]
    [MinLength(3)]
    [MaxLength(22)]
    public string Username { get; set; }
    [Required]
    [MinLength(8)]
    [MaxLength(64)]
    public string Password { get; set; }
    [Required]
    [EmailAddress]
    [MaxLength(320)]
    public string Email { get; set; }
}