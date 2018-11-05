using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using LearningApp.API;
using LearningApp.Core.Entities;
using LearningApp.Infrastructure;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Smart.Test
{
    public class UsersControllerTest
    {
        private Mock<ISecurityRepository<Users>> _usersRepo;
        private Mock<ISecurityRepository<UserRole>> _userRoleRepo;
        private Mock<ISecurityRepository<Roles>> _roleRepo;
        private Mock<IConfiguration> _config;
        private UsersController controller;

        // Arrange
        private string username = "user@gmail.com";
        private string password = "password";

        public UsersControllerTest()
        {
            _usersRepo = new Mock<ISecurityRepository<Users>>();
            _userRoleRepo = new Mock<ISecurityRepository<UserRole>>();
            _roleRepo = new Mock<ISecurityRepository<Roles>>();
            _config = new Mock<IConfiguration>();

            controller = new UsersController(_usersRepo.Object, _userRoleRepo.Object, _roleRepo.Object, _config.Object);


            //Arrange
            _usersRepo.Setup(repo => repo.GetAll()).Returns(new[]
                  {
                    new Users { Id = 1, Email = "user@gmail.com", Password = password
                  }}.AsQueryable());

            _roleRepo.Setup(repo => repo.GetAll()).Returns(new[]
            {
                    new Roles { Id = 1, Name = "RoleName"}
                }.AsQueryable());

            _userRoleRepo.Setup(repo => repo.GetAll()).Returns(new[]
               {
                    new UserRole { UserId = 1, RoleId = 1}
                }.AsQueryable());

            _config.SetupGet(m => m["Tokens:Key"]).Returns("0123456789ABCDEF");
            _config.SetupGet(m => m["Tokens:Issuer"]).Returns("http://mycodecamp.io");
        }
        [Fact]
        public void UserLogin_ReturnsSuccess_CorrectCredentials()
        {
            // Act
            var result = controller.Login(new LoginModel()
            { Email = username,
              Password = password }
            ) as IActionResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public void UserLogin_ReturnsFailure_IncorrectCrentials()
        {

            // Act
            var result = controller.Login(new LoginModel()
            {
                Email = username,
                Password = "WrongPassword"
            }
           ) as IActionResult;

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public void UserLogin_ReturnsSuccess_UserExists()
        {
            // Act
            var result = controller.GetUser(username) as IActionResult;

            // Assert
            Assert.IsType<OkObjectResult>(result);

        }
    }
}