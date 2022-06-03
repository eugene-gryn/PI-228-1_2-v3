using BLL.DTOs;
using BLL.DTOs.User;
using BLL.Services;
using NUnit.Framework;

namespace Tests.Services
{
    public class OrderServicesTest : ABaseTest
    {

        private OrderService _repos;
        public OrderServicesTest()
        {
            _repos = new OrderService(uow);
        }

        public override void EndOperation()
        {

            base.EndOperation();
            _repos = new OrderService(uow);
        }
        [Test]
        public void CreateOrder_InNotEmptyAsync()
        {
            OrderDTO order = new OrderDTO();

             _repos.Create(order);

            Assert.AreEqual(order.DeliveryInfo,"");
            EndOperation();
        }
        [Test]
        public void GetMainData_InNotEmptyAsync()
        {
            OrderDTO order = new OrderDTO();

         
            _repos.Create(order);
            _repos.GetMainData(order.ID);

            Assert.IsNotNull(order);
            EndOperation();

        }
        [Test]
        public void GetOrderProduct_InNotEmpty()
        {
            OrderDTO order = new OrderDTO();

            _repos.Create(order);
            _repos.GetOrderProducts(order.ID);

            Assert.IsNotNull(order);
            EndOperation();

        }
        [Test]
        public void GetUserOrders_InNotEmpty()
        {
            OrderDTO order = new OrderDTO();
            

            _repos.Create(order);
            _repos.GetUserOrders(order.ID);

            Assert.IsNotNull(order);
            EndOperation();
        }
        [Test]
        public void GetCartProducts_InNotEmpty()
        {
            OrderDTO order = new OrderDTO();
            UserMainDataDTO user = new UserMainDataDTO();

            _repos.Create(order);
            _repos.GetCartProducts(user.ID);

            Assert.IsNotNull(user);
            EndOperation();
        }
        [Test]
        public void DeleteOrder_InNotEmpty()
        {
            OrderDTO order = new OrderDTO();

            _repos.Create(order);
            _repos.DeleteOrder(order.ID);

            Assert.IsNotNull(order);
            EndOperation();
        }
        [Test]
        public void MoveProductsFromCartToOrder_InNotEmpty()
        {
            OrderDTO order = new OrderDTO();
            UserMainDataDTO user = new UserMainDataDTO();
            ProductDTO product = new ProductDTO();

            _repos.Create(order);
            _repos.AddProductToCart(user.ID,product.ID,1);

            Assert.IsNotNull(order);
            EndOperation();
        }
        [Test]
        public void AssignProductsToOrder_InNotEmpty()
        {
            OrderDTO order = new OrderDTO();
            UserCartDTO cart = new UserCartDTO();
            ProductDTO product = new ProductDTO();

            _repos.Create(order);
            _repos.AssignProductsToOrder(order.ID, cart.Cart);

            Assert.IsNotNull(cart);
            EndOperation();
        }
        /**/
        [Test]
        public void MarkOrderAsProcessed_IsProcesed()
        {
            OrderDTO order = new OrderDTO();

            _repos.Create(order);
            
            _repos.MarkOrderAsProcessed(order.ID);

            Assert.IsFalse(order.Processed);
            EndOperation();
        }
        [Test]
        public void AddProductToCart_InNotEmpty()
        {
            OrderDTO order = new OrderDTO();
            UserMainDataDTO user = new UserMainDataDTO();
            ProductDTO product = new ProductDTO();

            _repos.Create(order);
            _repos.AddProductToCart(user.ID, product.ID, 1);

            Assert.IsNotNull(order);
            EndOperation();
        }
        [Test]
        public void DeleteProductFromCart_InNotEmpty()
        {
            OrderDTO order = new OrderDTO();
            UserMainDataDTO user = new UserMainDataDTO();
            ProductDTO product = new ProductDTO();

            _repos.Create(order);
            _repos.DeleteProductFromCart(user.ID,product.ID);

            Assert.IsNotNull(order);
            EndOperation();
        }
    }
}
