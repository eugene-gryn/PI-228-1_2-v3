using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs;

public class ProductShortDTO
{
    public int ID { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? PhotoPath { get; set; }

    public float Price { get; set; }
    public int RemainingStock { get; set; }
}
