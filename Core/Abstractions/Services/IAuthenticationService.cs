using System.Threading.Tasks;

namespace Core.Abstractions.Services
{
    
    public interface IAuthenticationService
    {
        Task<string> GenerateAuthenticationAsync(string userEmail);
        Task<bool> ValidateCredentialsAsync(string email, string password);
    }
}
