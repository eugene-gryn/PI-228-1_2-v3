using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.DTOs.Product
{
    public class ProductAmountDTO
    {
        [Key]
        public int ID { get; set; }
        [Required]
        public int ProductID { get; set; }

        public ProductDTO? ProductDto { get; set; }

        public int Amount { get; set; } = 0;
    }
}
