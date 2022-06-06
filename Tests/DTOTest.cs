using BLL.DTOs;
using BLL.DTOs.Product;
using BLL.DTOs.User;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Tests
{
    public class DTOTest
    {
        [Test]
        public void CreateOrderDTO_IsNotNull()
        {
            OrderDTO order = new OrderDTO();

            order.ID = 1;
            order.UserID = 1;
            order.DeliveryInfo = "Test";
            order.Processed = true;

            Assert.IsNotNull(order);
        }
        [Test]
        public void СheckElementsInOrderCollection_IsNotEmpty()
        {
            OrderDTO order = new OrderDTO();

            order.ID = 1;
            order.UserID = 1;
            order.DeliveryInfo = "Test";
            order.Processed = true;

            Assert.IsNotNull(order);
        }
        [Test]
        public void CreateProductCreateDTO_IsNotNull()
        {
            ProductCreateDTO product = new ProductCreateDTO();

            product.Name = "Test";
            product.PhotoPath = "Test";
            product.Description = "Test";
            product.Price = 0;
            product.RemainingStock = 0;

            Assert.IsNotNull(product);
        }
        [Test]
        public void CreateProductDTO_IsNotNull()
        {
            ProductDTO product = new ProductDTO();

            product.ID = 1;
            product.Name = "Test";
            product.PhotoPath = "Test";
            product.Description = "Test";
            product.Price = 0;
            product.RemainingStock = 1;
            product.Views = 0;
            product.Purchase = 0;

            Assert.IsNotNull(product);
        }
        [Test]
        public void CreateProductShortDTO_IsNotNull()
        {
            ProductShortDTO product = new ProductShortDTO();

            product.ID = 1;
            product.Name = "Test";
            product.PhotoPath = "Test";
            product.Price = 0;
            product.RemainingStock = 1;

            Assert.IsNotNull(product);
        }
        [Test]
        public void CreateProductViewDTO_IsNotNull()
        {
            ViewsProductDTO product = new ViewsProductDTO();

            product.ID = 1;
            product.Views = 0;
            product.Purchase = 0;

            Assert.IsNotNull(product);
        }
        [Test]
        public void СheckElementsInProductCollection_IsNotEmpty()
        {
            ProductDTO product = new ProductDTO();

            product.ID = 1;
            product.Name = "Test";
            product.PhotoPath = "Test";
            product.Description = "Test";
            product.Price = 0;
            product.RemainingStock = 1;
            product.Views = 0;
            product.Purchase = 0;

            Assert.IsNotNull(product);
        }
        [Test]
        public void CreateUserCartDTO_IsNotNull()
        {
            UserCartDTO user = new UserCartDTO();

            user.Cart = new Dictionary<ProductDTO, int>();
            user.Cart.Add(new ProductDTO(), 1);

            Assert.That(user.Cart.Count, Is.EqualTo(1));
        }
        [Test]
        public void CreateUserLoginDTO_IsNotNull()
        {
            UserLoginDTO user = new UserLoginDTO();
          
            user.Email = "test@gmail.com";
            user.Password = "aaa";

            Assert.NotNull(user);
        }
        [Test]
        public void CreateUserMainDataDTO_IsNotNull()
        {
            UserMainDataDTO user = new UserMainDataDTO();

            user.ID = 1;
            user.Name = "Test";
            user.Email = "test@gmail.com";
            user.Phone = "+3800000000";
            user.RefreshToken = "Test";
            user.TokenCreated = DateTime.Now;
            user.TokenExpires = DateTime.Now.AddDays(1);
            user.IsModerator = true;
            user.IsAdmin = true;

            Assert.NotNull(user);
        }
        [Test]
        public void CreateUserOrdersDTO_IsNotEmoty()
        {
            UserOrdersDTO user = new UserOrdersDTO();

            user.Orders = new List<OrderDTO>();
            user.Orders.Add(new OrderDTO());

            Assert.That(user.Orders.Count, Is.EqualTo(1));
        }
        [Test]
        public void CreatUserRegisterDTO_IsNotNull()
        {
            UserRegisterDTO user = new UserRegisterDTO();

          
            user.Name = "Test";
            user.Email = "test@gmail.com";
            user.Phone = "+3800000000";
            user.Password = "aaa";

            Assert.NotNull(user);
        }
        [Test]
        public void СheckElementsInUserCollection_IsNotEmpty()
        {
            UserMainDataDTO user = new UserMainDataDTO();

            user.ID = 1;
            user.Name = "Test";
            user.Email = "test@gmail.com";
            user.Phone = "+3800000000";
            user.RefreshToken = "Test";
            user.TokenCreated = DateTime.Now;
            user.TokenExpires = DateTime.Now.AddDays(1);
            user.IsModerator = true;
            user.IsAdmin = true;

            Assert.IsNotNull(user);
        }
    }
}
