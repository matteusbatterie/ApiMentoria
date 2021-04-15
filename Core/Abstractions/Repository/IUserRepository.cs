using System.Collections.Generic;
using Core.Entities;

namespace Core.Abstractions.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User Get(int id);
        void Create(User user);
        void Update(User entity);
        void Delete(int id);
    }
}