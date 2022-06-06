using BLL.DTOs;
using BLL.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Services
{
    public class StatisticServiceTests : ABaseTest
    {
        private StatisticsService _repos;
        public StatisticServiceTests()
        {
            _repos = new StatisticsService(uow);
        }

        public override void EndOperation()
        {
            base.EndOperation();
            _repos = new StatisticsService(uow);
        }
        [Test]
        public void AddView_ChangeCount()
        {
            ProductDTO productDTO = new ProductDTO();

            _repos.AddView(1);

            Assert.IsNotNull(productDTO);
        }
        [Test]
        public void AddBought_ChangeCount()
        {
            ProductDTO productDTO = new ProductDTO();

            _repos.AddBought(1);

            Assert.IsNotNull(productDTO);
        }
        [Test]
        public void GetMostViewed_ViewProduct()
        {
            ProductDTO product = new ProductDTO();

            _repos.GetMostViewed();

            Assert.That(product, Is.Not.Null);
        }
        [Test]
        public void GetMostViewedTop_ViewProduct()
        {
            List<ProductDTO> products = new List<ProductDTO>();

            _repos.GetMostViewedTop(5);

            Assert.That(_repos.GetMostViewedTop(5), Is.Not.Null);
        }
        [Test]
        public void GetMostPurchased_ViewProduct()
        {
            List<ProductDTO> products = new List<ProductDTO>();

            _repos.GetMostPurchased();

            Assert.That(products, Is.Not.Null);
        }
        [Test]
        public void GetMostPurchasedTop_ViewProduct()
        {
            List<ProductDTO> products = new List<ProductDTO>();

            _repos.GetMostPurchasedTop(5);

            Assert.That(_repos.GetMostPurchasedTop(5), Is.Not.Null);
        }
    }
}
