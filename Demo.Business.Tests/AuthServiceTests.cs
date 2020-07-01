using AutoMapper;
using Demo.Business.AutoMapper;
using Demo.Business.Consumers;
using Demo.Business.Providers;
using Demo.Common.Cryptography;
using Demo.Common.DataModels;
using Demo.Common.ViewModels;
using Demo.Repository.Consumers;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Business.Tests
{
    public class AuthServiceTests
    {
        private IAuthService authService;
        private IUserRepository userRepository;
        private Task<IEnumerable<User>> users;
        private string LoginName = "Test";
        private string Password = "puKDlyFmww4=";

        [OneTimeSetUp]
        public void SetUp()
        {
            users = SetUpUsers();
        }

        [SetUp]
        public void ReInitializeTest()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MapperProfile());
            });
            var mapper = mockMapper.CreateMapper();
            userRepository = SetUpUserRepository();
            authService = new AuthService(mapper, userRepository);
        }

        

        [TearDown]
        public void DisposeTest()
        {
            userRepository = null;
            authService = null;
        }

        [OneTimeTearDown]
        public void DisposeAllObjects()
        {
            users = null;
        }
        [Test]
        public void GetUserTokenTest()
        {
            AuthUserInputModel authUser = new AuthUserInputModel();
            authUser.LoginName = LoginName;
            authUser.Password = Password;
            var output = authService.AuthenticateUser(authUser).Result;
            Assert.NotNull(output.Token);
        }

        private static Task<IEnumerable<User>> SetUpUsers()
        {
            var products = GetListAsync();
            return products;

        }
        private static Task<IEnumerable<User>> GetListAsync()
        {
            return Task.Run(() => TestHelper.GetUserList());
        }

        private IUserRepository SetUpUserRepository()
        {
            var mockRepo = new Mock<IUserRepository>(MockBehavior.Default);

            mockRepo.Setup(p => p.GetByCondition(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(users);

            return mockRepo.Object;
        }
    }
}
