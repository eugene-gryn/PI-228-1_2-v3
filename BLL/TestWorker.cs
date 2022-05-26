using DAL.EF;
using DAL.Entities;

namespace BLL;

public class TestWorker
{
    public TestWorker()
    {
        using (var context = new MainContext())
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            var products = new List<Product>
            {
                new()
                {
                    Name = "Computer",
                    Views = (uint) Random.Shared.Next(500),
                    Purchase = (uint) Random.Shared.Next(500),
                    Description = "PC",
                    RemainingStock = Random.Shared.Next(100)
                },


                new()
                {
                    Name = "Laptop",
                    Views = (uint) Random.Shared.Next(500),
                    Purchase = (uint) Random.Shared.Next(500),
                    Description = "Portable PC",
                    RemainingStock = Random.Shared.Next(100)
                },


                new()
                {
                    Name = "Phone",
                    Views = (uint) Random.Shared.Next(500),
                    Purchase = (uint) Random.Shared.Next(500),
                    Description = "hand PC",
                    RemainingStock = Random.Shared.Next(100)
                },


                new()
                {
                    Name = "Joker",
                    Views = (uint) Random.Shared.Next(500),
                    Purchase = (uint) Random.Shared.Next(500),
                    Description = "Card from minecraft",
                    RemainingStock = Random.Shared.Next(100)
                }
            };

            context.Products.AddRange(products);
            context.SaveChanges();


            var users = new List<User>
            {
                new()
                {
                    Name = "default",
                    Email = "default@email.com",
                    PasswordHash = new byte[] {0x00, 0x21, 0x60, 0x1F},
                    PasswordSalt = new byte[] {0x00, 0x21, 0x60, 0x1F},
                    Cart = new List<ProductAmount>
                        {new() {Product = products[0], Amount = 22}, new() {Product = products[1], Amount = 55}}
                },

                new()
                {
                    Name = "moderator",
                    Email = "moderator@email.com",
                    PasswordHash = new byte[] {0x00, 0x21, 0x60, 0x1F},
                    PasswordSalt = new byte[] {0x00, 0x21, 0x60, 0x1F},
                    Cart = new List<ProductAmount>
                        {new() {Product = products[0], Amount = 22}, new() {Product = products[1], Amount = 55}},
                    IsModer = true
                },

                new()
                {
                    Name = "admin",
                    Email = "admin@email.com",
                    PasswordHash = new byte[] {0x00, 0x21, 0x60, 0x1F},
                    PasswordSalt = new byte[] {0x00, 0x21, 0x60, 0x1F},
                    Cart = new List<ProductAmount>
                        {new() {Product = products[0], Amount = 22}, new() {Product = products[1], Amount = 55}},
                    IsAdmin = true
                }
            };

            context.Users.AddRange(users);
            context.SaveChanges();


            var orders = new List<Order>
            {
                new()
                {
                    UserID = users[0].ID,
                    ProductAmounts = new List<ProductAmount> {new() {Product = products[0], Amount = 33}}
                },
                new()
                {
                    UserID = users[1].ID,
                    ProductAmounts = new List<ProductAmount> {new() {Product = products[1], Amount = 33}}
                },
                new()
                {
                    UserID = users[2].ID,
                    ProductAmounts = new List<ProductAmount> {new() {Product = products[2], Amount = 33}}
                }
            };
            context.Orders.AddRange(orders);
            context.SaveChanges();
        }


        /*using (var context = new MainContext())
        {
            var user = context.Users.Include(u => u.Cart).ThenInclude(pa => pa.Product).First();
            Console.WriteLine($"Name: {user.Name}");

            var cart = user.Cart;


            Console.WriteLine($"Count in cart: {cart.Count}");


            foreach (var pa in cart) Console.WriteLine($"    {pa.Product.Name}: {pa.Amount} шт.");
        }*/
    }
}