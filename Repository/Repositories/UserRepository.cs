using DataAccess.DAO;
using DataAccess.Models;
using Repository.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class UserRepository : IUserRepository
    {
        public void DeleteUser(int id) => UserDAO.DeleteUser(id);

        public User GetUserByEmailAndPassword(string email, string password) => UserDAO.GetUserByEmailAndPassword(email, password);
        

        public User GetUserById(int id) => UserDAO.GetUserById(id);
        

        public List<User> GetUserByName(string username) => UserDAO.GetUserByName(username);


        public List<User> GetUsers() => UserDAO.GetUser();
        

        public void SaveUser(User user) => UserDAO.SaveUser(user);
        

        public void UpdateUser( User user) => UserDAO.UpdateUser(user);
        
    }
}
