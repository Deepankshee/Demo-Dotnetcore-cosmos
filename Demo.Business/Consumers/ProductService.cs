using AutoMapper;
using Demo.Business.Providers;
using Demo.Common.Constants;
using Demo.Common.DataModels;
using Demo.Common.ViewModels;
using Demo.Repository.Providers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Demo.Business.Consumers
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository = null;
        private readonly IMapper mapper = null;
        public ProductService(IMapper mapper, IProductRepository productRepository)
        {
            this.mapper = mapper;
            this.productRepository = productRepository;
        }
        public async Task<bool> Create(ProductViewModel product)
        {
            try
            {
                var productData = mapper.Map<Product>(product);
                productData.BasePartitionKey = GlobalConstants.BasePartitionKey;
                productData.DocumentType = DocumentType.Product;
                dynamic[] products = new dynamic[]
                 {
                new {
                    userId = productData.UserId,
                    name = productData.Name,
                    price = productData.Price,
                    basePartitionKey = productData.BasePartitionKey,
                    documentType = productData.DocumentType
                }
                 };
                return await productRepository.CreateBySP(products);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ProductListViewModel> GetProductsByUserId(string userId)
        {
            try
            {
                var productData =  await productRepository.GetProductsByUserId(userId);
                ProductListViewModel products = null ;
                if (productData != null && productData.Count > 0)
                {
                    products = new ProductListViewModel();
                    products.FirstName = productData[0].FirstName;
                    products.LastName = productData[0].LastName;
                    products.Products = new List<ProductViewModel>();
                    foreach (var item in productData)
                    {
                        var product = new ProductViewModel();
                        product.Id = item.Id;
                        product.Name = item.Name;
                        product.Price = item.Price;
                        product.UserId = item.UserId;
                        products.Products.Add(product);
                    }
                }
                return products;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
