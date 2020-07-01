using System;
using System.Collections.Generic;
using System.Text;
using Demo.Business.Consumers;
using Demo.Business.Providers;
using Demo.Common.DataModels;
using Demo.Repository.Consumers;
using NUnit.Framework;
using Moq;
using System.Threading.Tasks;
using Demo.Repository.Providers;
using Demo.Common.ViewModels;

namespace Demo.Business.Tests
{
    public class ProductServiceTests
    {
        private IProductService _productService;
        private Task<List<ProductListModel>> _products;
        private IProductRepository _productRepository;
        Guid obj = Guid.NewGuid();


        [OneTimeSetUp]
        public void SetUp()
        {
            _products = SetUpProducts();
        }

        

        [SetUp]
        public void ReInitializeTest()
        {
            _productRepository = SetUpProductRepository();
            _productService = new ProductService(null, _productRepository);
        }

        [TearDown]
        public void DisposeTest()
        {
            _productService = null;
            _productRepository = null;
        }

        [OneTimeTearDown]
        public void DisposeAllObjects()
        {
            _products = null;
        }

        [Test]
        public void GetAllProductsTest()
        {
            var products = _productService.GetProductsByUserId(obj.ToString()).Result;
            var compareData = TestHelper.GetProductList();
            Assert.AreEqual(products.GetType(), typeof(ProductListViewModel));
            Assert.AreEqual(products.FirstName, compareData[0].FirstName);
            Assert.AreEqual(products.Products.Count, compareData.Count);
        }

        [Test]
        public void GetAllProductsTestForNull()
        {
            _products.Result.Clear();
            var products = _productService.GetProductsByUserId(obj.ToString()).Result;
            Assert.Null(products);
            SetUpProducts();
        }

        #region private Methods
        private IProductRepository SetUpProductRepository()
        {  
            var mockRepo = new Mock<IProductRepository>(MockBehavior.Default);
            
            mockRepo.Setup(p => p.GetProductsByUserId(obj.ToString())).Returns(_products);
            return mockRepo.Object;
        }

        private static Task<List<ProductListModel>> SetUpProducts()
        {
            var products = GetListAsync();
            return products;

        }

        private static Task<List<ProductListModel>> GetListAsync()
        {
            return Task.Run(() => TestHelper.GetProductList());
        }

        #endregion
    }
}
