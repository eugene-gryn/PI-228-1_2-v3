using BLL.Services;
using DAL.EF;
using DAL.UOW;
using Microsoft.EntityFrameworkCore;

namespace Tests.Services
{
    public class OrderServicesTest : ABaseTest
    {
        private OrderService _repos;

        public override void EndOperation()
        {
            base.EndOperation();
            _repos = new OrderService(uow);
        }
    }
}
