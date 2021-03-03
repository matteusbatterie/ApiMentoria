using Moq;
using Xunit;

using Core.Entities;
using Core.Abstractions.Repository;

using Service;
using Repository;

namespace Tests
{
    public class UserService_Create
    {
        #region Name
        [Theory]

        [InlineData(false, "ABC")]
        [InlineData(false, "123")]
        [InlineData(false, "123456789")]
        [InlineData(false, "teste")]
        [InlineData(false, "")]
        [InlineData(false, "                        ")]

        [InlineData(true, "Nome com caractere especial e espaço")]
        [InlineData(true, "Teste acento ãõ")]
        [InlineData(true, "João da Silva Pereira")]
        public void CreateUserValidateUserNameTests(bool outcome, string name)
        {
            // Arrange
            Mock<UserRepository> userRepositoryMock = new Mock<UserRepository>();
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
            Assert.Equal(result, outcome);
        }
        #endregion

        #region Email
        [Theory]

        [InlineData(false, "teste")]
        [InlineData(false, "10")]
        [InlineData(false, "email")]
        [InlineData(false, "@")]
        [InlineData(false, "email.teste")]
        [InlineData(false, "email@")]
        [InlineData(false, "@teste")]
        [InlineData(false, "email @ teste")]
        [InlineData(false, "email@teste ")]
        [InlineData(false, " email@teste")]
        [InlineData(false, " ")]

        [InlineData(true, "email@teste")]
        [InlineData(true, "teste@gmail.com")]
        public void CreateUserValidateUserEmailTests(bool outcome, string email)
        {
            // Arrange
            Mock<UserRepository> userRepositoryMock = new Mock<UserRepository>();
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
            Assert.Equal(result, outcome);
        }
        #endregion

        #region CPF
        [Theory]

        [InlineData(false, "577.284.690-60")]
        [InlineData(false, "577284.690-60")]
        [InlineData(false, "577284690-60")]
        [InlineData(false, "577.284690-60")]
        [InlineData(false, "577.28469060")]
        [InlineData(false, "577.284.69060")]
        [InlineData(false, "57728469060")]

        [InlineData(true, "577.284.690-61")]
        [InlineData(true, "577284.690-61")]
        [InlineData(true, "577.284690-61")]
        [InlineData(true, "577.284.69061")]
        [InlineData(true, "57728469061")]
        public void CreateUserValidateUserCpfTests(bool outcome, string cpf)
        {
            // Arrange
            Mock<UserRepository> userRepositoryMock = new Mock<UserRepository>();
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
            Assert.Equal(result, outcome);
        }
        #endregion
    }
}