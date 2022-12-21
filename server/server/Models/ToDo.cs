using System.ComponentModel.DataAnnotations;
using IdGen;

namespace server.Models;

public class ToDo
{
    public long Id { get; set; }
    [Required]
    [MaxLength(30)]
    [MinLength(1)]
    public string Name { get; set; }
    [Required]
    [MinLength(1)]
    [MaxLength(255)]
    public string Description { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? CompletedAt { get; set; } = null;
    [Required]
    public long UserId { get; set; }
    public User User { get; set; }
}