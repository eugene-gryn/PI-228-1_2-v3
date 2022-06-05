using DAL.EF;
using DAL.Entities;
using DAL.UOW;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace Tests
{
   
    public abstract class ABaseTest 
    {
        protected MainContext _context;
        protected IUnitOfWork uow;
        protected DbContextOptions<MainContext> _configuration;

        #region
        private const string user1login = "aboba1@email.com";
        private const string user2login = "aboba2@email.com";
        private const string user3login = "aboba3@email.com";

        private const string user1password = "aaa1";
        private const string user2password = "aaa2";
        private const string user3password = "aaa3";
        #endregion

        public ABaseTest()
        {
            _configuration = new DbContextOptionsBuilder<MainContext>()
            .UseInMemoryDatabase(databaseName: "DbTest")
            .Options;
            _context = new MainContext(_configuration);
            uow = new EFUnitOfWork(_context);
        }
        private static KeyValuePair<byte[], byte[]> GeneratePassword(string password)
        {
            byte[] passwordHash, passwordSalt;


            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }


            return new KeyValuePair<byte[], byte[]>(key: passwordHash, value: passwordSalt);
        }
        public virtual void EndOperation()
        {
            uow.Dispose();
            _context = new MainContext(_configuration);
            uow = new EFUnitOfWork(_context);
        }

        

        [OneTimeSetUp]
        public void SetUp()
        {
            _context.Database.EnsureCreated();

            SeedDatabase();
        }

        [OneTimeTearDown]
        public void CleanUp()
        {
            _context.Database.EnsureDeleted();
        }


        public void SeedDatabase()
        {
            var orders = new List<Order>()
            {
                new Order()
                {
                    ID = 1,
                    UserID = 1,
                    DeliveryInfo = "",
                    Processed = true,
                    ProductAmounts = new List<ProductAmount>(),
                },
                new Order()
                {
                    ID = 2,
                    UserID = 1,
                    DeliveryInfo = "",
                    Processed = true,
                    ProductAmounts = new List<ProductAmount>(),

                },
                new Order()
                {
                    ID = 3,
                    UserID = 1,
                    DeliveryInfo = "",
                    Processed = true,
                    ProductAmounts = new List<ProductAmount>(),
                },
            };
            _context.Orders.AddRange(orders);
            _context.SaveChanges();

            var products = new List<Product>()
            {
                
                new Product()
                {
                    ID = 1,
                    Name = "",
                    PhotoPath = "",
                    Description = "",
                    Price = 1f,
                    RemainingStock = 1,
                    Views = 0,
                    Purchase = 0,
                },
                new Product()
                {
                    ID = 2,
                    Name = "",
                    PhotoPath = "",
                    Description = "",
                    Price = 1f,
                    RemainingStock = 1,
                    Views = 0,
                    Purchase = 0,
                },
                new Product()
                {
                    ID = 3,
                    Name = "",
                    PhotoPath = "",
                    Description = "",
                    Price = 1f,
                    RemainingStock = 1,
                    Views = 0,
                    Purchase = 0,
                }
            };
            _context.Products.AddRange(products);

            var user1 = GeneratePassword(user1password);
            var user2 = GeneratePassword(user2password);
            var user3 = GeneratePassword(user3password);

            var users = new List<User>()
            {
                new User()
                {
                    ID = 1,
                    Name = "test1",
                    Email = "test1@gmail.com",
                    Phone = "+380970000000",
                    IsAdmin = false,
                    IsModerator = false,
                    PasswordHash = user1.Key,
                    PasswordSalt = user1.Value,
                    Cart = new List<ProductAmount>
                    {new() {ProductID = products[0].ID, Amount = 22}, new() {ProductID = products[1].ID, Amount = 55}},

                },
            };
            _context.Users.AddRange(users);

            _context.SaveChanges();
        }

    }
}
