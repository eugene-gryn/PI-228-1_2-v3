using BLL.DTOs;
using DAL.Entities;
using DAL.UOW;
using System.Data.Entity;

namespace BLL.Services
{
    public class OrderService : AService
    {
        public OrderService(IUnitOfWork uow) : base(uow)
        {

        }

        public async Task<OrderDTO> Create(OrderDTO orderDto)
        {
            var order = Mapper.Map<Order>(orderDto);

            Database.Orders.Create(order);
            Database.Save();

            return await GetMainData(order.ID);
        }
        public async Task<OrderDTO?> GetMainData(int OrderID)
        {
            var order = await Database.Orders.Read().FirstOrDefaultAsync(ord => ord.ID == OrderID);
            if(order == null)
            {
                return null;
            }
            return Mapper.Map<OrderDTO>(order);
        }
    }
}
