using BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Services
{
    public class ProductServiceTest : ABaseTest
    {
        private ProductService _repos;

        public override void EndOperation()
        {
            base.EndOperation();
            _repos = new ProductService(uow);
        }
    }
}
