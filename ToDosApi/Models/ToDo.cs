using System.ComponentModel.DataAnnotations;

namespace ToDosApi.Models;

public class ToDo
{
    [Key]
    public int Id { get; set; }
    public string Task { get; set; } = null!;
    public DateTime? CreatedDateTime { get; set; }
    public DateTime? CompletedDateTime { get; set; }
}