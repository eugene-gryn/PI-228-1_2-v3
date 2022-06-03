using System.ComponentModel.DataAnnotations;

namespace DAL.Entities;

public class ProductAmount
{
    [Key]
    public int ID { get; set; }
    [Required]
    public int ProductID { get; set; }

    public int Amount { get; set; } = 0;
}