using System.Collections.Generic;
using Core.Entities;

namespace Core.Abstractions.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> Retrieve();
        User Retrieve(int id);
        void Create(User user);
        void Update(User entity);
        void Delete(int id);
    }
}