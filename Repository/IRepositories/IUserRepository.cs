using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        User GetUserById(int id);
        List<User> GetUserByName(string username);
        void SaveUser(User user);
        void DeleteUser(int id);
        void UpdateUser(int id , User user);
        User GetUserByEmailAndPassword(string email, string password);
    }
}
