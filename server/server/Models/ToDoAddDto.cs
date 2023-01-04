using System.ComponentModel.DataAnnotations;

namespace server.Models;

public class ToDoAddDto
{
    [Required]
    [MaxLength(30)]
    [MinLength(1)]
    public string Name { get; set; }
    [Required]
    [MinLength(1)]
    [MaxLength(255)]
    public string Description { get; set; }
}