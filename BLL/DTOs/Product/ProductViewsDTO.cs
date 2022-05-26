using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs;

public class ViewsProductDTO
{
    public int ID { get; set; }
    public uint Views { get; set; } = 0;
    public uint Purchase { get; set; } = 0;
}
