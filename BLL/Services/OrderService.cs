using System.Data.Entity;
using BLL.DTOs;
using DAL.Entities;
using DAL.UOW;

namespace BLL.Services;

public class OrderService : AService
{
    public OrderService(IUnitOfWork uow) : base(uow) { }

    
    public async Task<OrderDTO?> Create(OrderDTO orderDto)
    {
        var order = Mapper.Map<Order>(orderDto);

        Database.Orders.Create(order);
        Database.Save();

        return await GetMainData(order.ID);
    }

    
    public async Task<OrderDTO?> GetMainData(int orderID)
    {
        var order = await Database.Orders.Read().AsNoTracking().FirstOrDefaultAsync(ord => ord.ID == orderID);
        if (order == null) return null;
        return Mapper.Map<OrderDTO>(order);
    }


    public async Task<Dictionary<ProductDTO, int>?> GetOrderProducts(int orderID)
    {
        var order = await Database.Orders.Read()
            .Include(o => o.ProductAmounts)
            .FirstOrDefaultAsync(o => o.ID == orderID);

        if (order == null) return null;
        
        var dict = order.ProductAmounts.ToDictionary(
            keySelector: prodAm => Mapper.Map<ProductDTO>(prodAm.Product),
            elementSelector: prodAm => prodAm.Amount);

        return dict;


    }
}