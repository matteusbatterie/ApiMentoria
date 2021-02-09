using ApiMentoria.Models;

using System.Collections.Generic;

namespace ApiMentoria.Class_Library.Interfaces
{
    public interface IUserDAL
    {
        IEnumerable<User> GetAllUsers();
        void AddUser(User user);
        void UpdateUser(User user);
        User GetUser(int? id);
        void DeleteUser(int? id);
    }
}