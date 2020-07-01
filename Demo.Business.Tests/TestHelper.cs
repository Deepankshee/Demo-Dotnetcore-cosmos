using Demo.Common.DataModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.Tests
{
    public class TestHelper
    {
        public static List<ProductListModel> GetProductList()
        {
            var products = new List<ProductListModel>
                {
                new ProductListModel()
                {
                   FirstName = "DJ",
                   LastName = "Jain",
                   Id = new Guid(),
                   UserId = new Guid(),
                   Name = "Test",
                   Price = 1.35M    
                },
                new ProductListModel()
                {
                   FirstName = "DJ",
                   LastName = "Jain",
                   Id = new Guid(),
                   UserId = new Guid(),
                   Price = 1.35M
                },
            };
            return products;
        }

        public static IEnumerable<User> GetUserList()
        {
            var users = new List<User>
            {
                new User
                {
                    Id = new Guid(),
                    LoginName = "Test",
                    Password = "puKDlyFmww4=",
                    FirstName = "DJ",
                    LastName = "Jain",
                    Occupation = "SE"
                }
            };
            return users;
        }
    }
}
