using DAL.EF;
using DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace BLL;

public class TestWorker
{
    public TestWorker()
    {
        using (var context = new MainContext())
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();


            var p1 = new Product {Name = "Computer"};
            var p2 = new Product {Name = "Laptop"};
            context.Products.Add(p1);
            context.Products.Add(p2);
            context.SaveChanges();


            var u1 = new User
            {
                Name = "TestUser",
                Email = "test@email.com",
                PasswordHash = new byte[] {0x00, 0x21, 0x60, 0x1F},
                PasswordSalt = new byte[] {0x00, 0x21, 0x60, 0x1F},
                Cart = new List<ProductAmount> {new() {Product = p1, Amount = 22}, new() {Product = p2, Amount = 55}}
            };
            context.Users.Add(u1);
            context.SaveChanges();


            var o1 = new Order
                {UserID = u1.ID, ProductAmounts = new List<ProductAmount> {new() {Product = p2, Amount = 33}}};
            context.Orders.Add(o1);
            context.SaveChanges();
        }


        using (var context = new MainContext())
        {
            var user = context.Users.Include(u => u.Cart).ThenInclude(pa => pa.Product).First();
            Console.WriteLine($"Name: {user.Name}");

            var cart = user.Cart;


            Console.WriteLine($"Count in cart: {cart.Count}");


            foreach (var pa in cart) Console.WriteLine($"    {pa.Product.Name}: {pa.Amount} шт.");
        }
    }
}