using System.ComponentModel.DataAnnotations;

namespace BLL.DTOs;

public class ProductDTO
{
    public int ID { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    public string? PhotoPath { get; set; }
    [Required]
    public string Description { get; set; } = string.Empty;
    [Required]
    public float Price { get; set; }
    public int RemainingStock { get; set; }
}