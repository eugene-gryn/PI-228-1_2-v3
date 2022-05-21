using BLL.DTOs;
using DAL.UOW;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class ProductService : AService
    {
        public ProductService(IUnitOfWork uow) : base(uow)
        {

        }

        
    }
}
