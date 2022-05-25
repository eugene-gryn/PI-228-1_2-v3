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

        await Database.Orders.Create(order);
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
            .AsNoTracking()
            .Include(o => o.ProductAmounts)
            .FirstOrDefaultAsync(o => o.ID == orderID);

        if (order == null) return null;
        
        var dict = order.ProductAmounts.ToDictionary(
            keySelector: prodAm => Mapper.Map<ProductDTO>(prodAm.Product),
            elementSelector: prodAm => prodAm.Amount);

        return dict;
    }
    
    
    public async Task<Dictionary<ProductDTO, int>?> GetCartProducts(int cartUserID)
    {
        var user = await Database.Users.Read()
            .AsNoTracking()
            .Include(u => u.Cart)
            .FirstOrDefaultAsync(u => u.ID == cartUserID);

        if (user == null) return null;

        var dict = user.Cart.ToDictionary(
            keySelector: prodAm => Mapper.Map<ProductDTO>(prodAm.Product),
            elementSelector: prodAm => prodAm.Amount);

        return dict;
    }
    
    
    public async Task<bool> DeleteOrder(int orderID)
    {
        var success = await Database.Orders.Delete(orderID);//TODO check ProdAmounts also are deleted
        if (success) Database.Save();

        return success;
    }

    public async Task<bool> MoveProductsFromCartToOrder(int cartUserID, int orderID)
    {
        var cartDict = await GetCartProducts(cartUserID);
        if (cartDict == null || cartDict.Count == 0)
            return false;

        var order = await Database.Orders.Read()
            .Include(o => o.ProductAmounts)
            .FirstOrDefaultAsync(u => u.ID == orderID);
        if (order == null) return
            false;

        var user = await Database.Users.Read()
            .Include(u => u.Cart)
            .FirstOrDefaultAsync(u => u.ID == cartUserID);


        var userCart = user.Cart;

        foreach (var productAmount in userCart)
        {
            user.Cart.Remove(productAmount);
            order.ProductAmounts.Add(productAmount);
        }
        
        Database.Save();
        return true;
    }

    //может и не нужен
    public async Task<bool> AssignProductsToOrder(int orderID, Dictionary<ProductDTO, int> dict)
    {
        throw new NotImplementedException();
    }



    public async Task<bool> MarkOrderAsProcessed(int orderID)
    {
        var order = await Database.Orders.Read()
            .FirstOrDefaultAsync(u => u.ID == orderID);

        if (order == null) return false;

        order.Processed = true;
        Database.Save();
        return true;

    }



    public async Task<bool> AddProductToCart(int cartUserID, int productID, int amount)
    {
        var user = await Database.Users.Read()
            .Include(u => u.Cart)
            .FirstOrDefaultAsync(u => u.ID == cartUserID);

        if (user == null) return false;

        var product = await Database.Products.Read()
            .AsNoTracking()
            .FirstOrDefaultAsync(p => p.ID == productID);

        if (product == null) return false;

        user.Cart.Add(new ProductAmount(){Product = product, Amount = amount});
        Database.Save();
        return true;
    }
    
    
    public async Task<bool> DeleteProductFromCart(int cartUserID, int productID)
    {
        var user = await Database.Users.Read()
            .Include(u => u.Cart)
            .FirstOrDefaultAsync(u => u.ID == cartUserID);

        if (user == null) return false;

        var product = user.Cart.FirstOrDefault(pa => pa.Product.ID == productID);
        if (product == null) return false;

        user.Cart.Remove(product);
        Database.Save();
        return true;
    }

}