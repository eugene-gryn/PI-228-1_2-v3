using System.ComponentModel.DataAnnotations;

namespace DAL.Entities;

public class Product
{
    public int ID { get; set; }
    [Required]
    public string Name { get; set; } = string.Empty;
    public string? PhotoPath { get; set; }
    public string Description { get; set; } = string.Empty;
    public float Price { get; set; }
    public int RemainingStock { get; set; }
}