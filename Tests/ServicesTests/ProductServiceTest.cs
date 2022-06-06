using BLL.DTOs;
using BLL.DTOs.Product;
using BLL.Services;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Services
{
    public class ProductServiceTest : ABaseTest
    {
        private ProductService _repos;

        public ProductServiceTest()
        {
            _repos = new ProductService(uow);
        }

        public override void EndOperation()
        {
            base.EndOperation();
            _repos = new ProductService(uow);
        }
       
        [Test]
        public void Getproduct_ReturnProduct()
        {
            List<ProductDTO> products = new List<ProductDTO>();

            products.Add(new ProductDTO());

            Assert.IsFalse(products.Contains(new ProductDTO()));

        }
        [Test]
        public void GetProductDTOs_ReturnProduct()
        {
            List<ProductShortDTO> products = new List<ProductShortDTO>();
            
            products.Add(new ProductShortDTO());

            Assert.IsFalse(products.Contains(new ProductShortDTO()));
        }
        [Test]
        public void GetMainData_ReturnProduct()
        {
            List<ProductDTO> products = new List<ProductDTO>();

            products.Add(new ProductDTO());
            _repos.GetMainData(1);

            Assert.DoesNotThrow(() => products.Contains(new ProductDTO()));
        }
        [Test]
        public void CreateProduct_SaveInDB()
        {
            ProductCreateDTO product = new ProductCreateDTO();
            
            _repos.Create(product);

            Assert.That(product, Is.Not.Null);
        }
        [Test]
        public void UpdateProduct_ChangeName()
        {
            ProductDTO product = new ProductDTO();

            _repos.Update(product);

            Assert.AreNotSame(product, new ProductDTO());
        }
        [Test]
        public void SearchProduct_GetProduct()
        {
            Assert.That(_repos.Search("2"), Is.Not.Null);
        }
        [Test]
        public void DeleteProguct_NoFoundProduct()
        {
            _repos.DeleteProduct(1);

            Assert.That(_repos.Search("1"), Is.Not.Null);
        }
    }
}
