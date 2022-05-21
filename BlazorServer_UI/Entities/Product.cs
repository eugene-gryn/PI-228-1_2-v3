using System.ComponentModel.DataAnnotations;

namespace BlazorServer_UI.Entities;

public class Product
{
    public Product(int id, string name, string? photoPath, string description, float price, int remainingStock)
    {
        ID = id;
        Name = name;
        PhotoPath = photoPath;
        Description = description;
        Price = price;
        RemainingStock = remainingStock;
    }

    public int ID { get; set; }
    public string Name { get; set; }
    public string? PhotoPath { get; set; }
    public string Description { get; set; }
    public float Price { get; set; }
    public int RemainingStock { get; set; }
}