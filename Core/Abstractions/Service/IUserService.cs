using System.Collections.Generic;
using Core.Entities;

namespace Core.Abstractions.Service
{
    public interface IUserService
    {
        IEnumerable<User> Retrieve();
        User Retrieve(int id);
        void Create(User user);
        void Update(User user);
        void Delete(int id);
    }
}