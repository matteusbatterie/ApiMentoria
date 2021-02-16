using Moq;
using Xunit;

using ApiMentoria.Service;
using ApiMentoria.Models;
using ApiMentoria.Repository.Interface;
using ApiMentoria.Repository;

namespace Tests
{
    public class UserService_Create
    {
        #region Name
        [Theory]
        [InlineData("ABC")]
        [InlineData("123")]
        [InlineData("123456789")]
        [InlineData("teste")]
        [InlineData("")]
        [InlineData("                        ")]
        public void Create_InvalidName_ReturnFalse(string name)
        {
            // Arrange
            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.Create(It.IsAny<User>())).Returns(true);

            var userService = new UserService(userRepositoryMock.Object);
            var user = new User()
            {
                Name = name,
                Email = "test@email",
                Password = "test_password",
                CPF = "577.284.690-61",
            };

            // Act
            var result = userService.Create(user);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("Nome com caractere especial e espaço")]
        [InlineData("Teste acento ãõ")]
        [InlineData("João da Silva Pereira")]
        public void Create_ValidName_ReturnTrue(string name)
        {
            // Arrange
            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.Create(It.IsAny<User>())).Returns(true);

            var userService = new UserService(userRepositoryMock.Object);
            var user = new User()
            {
                Name = name,
                Email = "test@email",
                Password = "test_password",
                CPF = "577.284.690-61",
            };

            // Act
            var result = userService.Create(user);

            // Assert
            Assert.True(result);
        }
        #endregion

        #region Email
        [Theory]
        [InlineData("teste")]
        [InlineData("10")]
        [InlineData("email")]
        [InlineData("@")]
        [InlineData("email.teste")]
        [InlineData("email@")]
        [InlineData("@teste")]
        [InlineData("email @ teste")]
        [InlineData("email@teste ")]
        [InlineData(" email@teste")]
        [InlineData(" ")]
        public void Create_InvalidEmail_ReturnFalse(string email)
        {
            // Arrange
            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.Create(It.IsAny<User>())).Returns(true);

            var userService = new UserService(userRepositoryMock.Object);
            var user = new User()
            {
                Name = "Testing 10 characters",
                Email = email,
                Password = "test_password",
                CPF = "577.284.690-61",
            };

            // Act
            var result = userService.Create(user);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("email@teste")]
        [InlineData("email@teste.com")]
        public void Create_ValidEmail_ReturnTrue(string email)
        {
            // Arrange
            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.Create(It.IsAny<User>())).Returns(true);

            var userService = new UserService(userRepositoryMock.Object);
            var user = new User()
            {
                Name = "Testing 10 characters",
                Email = email,
                Password = "test_password",
                CPF = "577.284.690-61",
            };

            // Act
            var result = userService.Create(user);

            // Assert
            Assert.True(result);
        }
        #endregion

        #region CPF
        // Unificar os dois testes em um método só
        [Theory]
        [InlineData("1")]
        [InlineData("20")]
        [InlineData("300")]
        [InlineData(".")]
        [InlineData("")]
        [InlineData("a")]
        [InlineData("á")]
        [InlineData("!")]
        [InlineData("577.284.690-60")]
        [InlineData("577284.690-60")]
        [InlineData("577.284690-60")]
        [InlineData("577.284.69060")]
        public void Create_InvalidCpf_ReturnFalse(string cpf)
        {
            // Arrange
            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.Create(It.IsAny<User>())).Returns(true);

            var userService = new UserService(userRepositoryMock.Object);
            var user = new User()
            {
                Name = "Testing 10 characters",
                Email = "test@email",
                Password = "test_password",
                CPF = cpf,
            };

            // Act
            var result = userService.Create(user);

            // Assert
            Assert.False(result);
        }

        [Theory]
        [InlineData("577.284.690-61")]
        [InlineData("577284.690-61")]
        [InlineData("577.284690-61")]
        [InlineData("577.284.69061")]
        [InlineData("57728469061")]
        public void Create_ValidCpf_ReturnTrue(string cpf)
        {
            // Arrange
            Mock<IUserRepository> userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(x => x.Create(It.IsAny<User>())).Returns(true);

            var userService = new UserService(userRepositoryMock.Object);
            var user = new User()
            {
                Name = "Testing 10 characters",
                Email = "test@email",
                Password = "test_password",
                CPF = cpf,
            };

            // Act
            var result = userService.Create(user);

            // Assert
            Assert.True(result);
        }
        #endregion
    }
}