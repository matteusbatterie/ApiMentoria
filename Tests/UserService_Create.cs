using Moq;
using Xunit;

using Core.Entities;
using Core.Abstractions.Repositories;

using Core.Services;

namespace Tests
{
    public class UserService_Create
    {
        private readonly Mock<IUserRepository> _repository;
        public UserService_Create()
        {
            _repository = new Mock<IUserRepository>();
            _repository.Setup(x => x.Create(It.IsAny<User>()));
        }

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
            var userService = new UserService(_repository.Object);
            var user = new User()
            {
                Name = name,
                Email = "test@email",
                Password = "test_password",
                CPF = "577.284.690-61",
            };

            // Act
            userService.Create(user);

            // Assert
            //Assert.;
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
            var userService = new UserService(_repository.Object);
            var user = new User()
            {
                Name = "Testing 10 characters",
                Email = email,
                Password = "test_password",
                CPF = "577.284.690-61",
            };

            // Act
            userService.Create(user);

            // Assert
            //Assert.Equal(result, outcome);
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
            var userService = new UserService(_repository.Object);
            var user = new User()
            {
                Name = "Testing 10 characters",
                Email = "test@email",
                Password = "test_password",
                CPF = cpf,
            };

            // Act
            userService.Create(user);

            // Assert
            //Assert.Equal(result, outcome);
        }
        #endregion
    }
}