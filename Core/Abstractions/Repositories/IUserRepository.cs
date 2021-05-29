using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Abstractions.Repositories
{
    public interface IUserRepository
    {
        IEnumerable<User> Retrieve();
        User Retrieve(int id);
        void Create(User user);
        void Update(User entity);
        void Delete(int id);

        Task<User> GetUserByEmailAsync(string email);
    }
}