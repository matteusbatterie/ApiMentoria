using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

using Xunit;
using Moq;

using ApiMentoria.Controllers;
using ApiMentoria.Repository.Interface;
using ApiMentoria.Models;

namespace Tests
{
    public class UserController_Create
    {
        // [Fact]
        // [InlineData("1")]
        // [InlineData("20")]
        // [InlineData("300")]
        // [InlineData(".")]
        // [InlineData("")]
        // [InlineData("a")]
        // [InlineData("á")]
        // [InlineData("!")]
        // [InlineData("577.284.690-60")]
        // [InlineData("577284.690-60")]
        // [InlineData("577.284690-60")]
        // [InlineData("577.284.69060")]
        // public void Create_InvalidCpf_ReturnFalse(string cpf)
        // {
        //     //Given
        //     Mock<ILogger<UserController>> loggerUserControllerMock = new Mock<ILogger<UserController>>();
        //     Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();
        //     userRepositoryMock.Setup(x => x.Create(It.IsAny<User>())).Returns(true);

        //     var userController = new UserController(loggerUserControllerMock.Object, userRepositoryMock.Object);
        //     var user = new User()
        //     {
        //         Name = "Testing 10 characters",
        //         Email = "test@email",
        //         Password = "test_password",
        //         CPF = cpf,
        //     };

        //     //When
        //     var data = userController.Create(user);

        //     //Then
        //     Assert.IsType<OkObjectResult>(data);
        // }

        // [Fact]
        // public void Create_InvalidCpf_ReturnTrue()
        // {
        //     //Given
        //     Mock<ILogger<UserController>> loggerUserControllerMock = new Mock<ILogger<UserController>>();
        //     Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();

        //     userRepositoryMock.Setup(x => x.Create(It.IsAny<User>())).Returns(true);
        //     var userController = new UserController(loggerUserControllerMock.Object, userRepositoryMock.Object);
        //     var user = new User()
        //     {
        //         Name = "Testing 10 characters",
        //         Email = "test2@email",
        //         Password = "test_password2",
        //         CPF = "395.040.220-90",
        //     };

        //     //When
        //     var data = userController.Create(user);

        //     //Then
        //     Assert.IsType<OkObjectResult>(data);
        // }
    }
}