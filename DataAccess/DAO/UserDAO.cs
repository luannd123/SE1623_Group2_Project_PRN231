using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class UserDAO
    {
        public static List<User> GetUser()
        {
            List<User> users = new List<User>();
            using (var context = new StoreDBContext())
            {
                users = context.Users.ToList();
            }
            return users;
        }

        public static User GetUserById(int id)
        {
            User user = new User();
            using (var context = new StoreDBContext())
            {
                user = context.Users.SingleOrDefault(x => x.UserId == id);
            }
            return user;
        }

        public static List<User> GetUserByName(string name)
        {
            List<User> users = new List<User>();
            using (var context = new StoreDBContext())
            {
                users = context.Users.Where(x => x.UserName.ToLower().Contains(name.ToLower())).ToList();
            }
            return users;
        }

        public static void SaveUser(User user)
        {
            try
            {
                using (var context = new StoreDBContext())
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public static void DeleteUser(int id)
        {
            try
            {
                using (var context = new StoreDBContext())
                {
                    var result = context.Users.SingleOrDefault(x => x.UserId == id);
                    context.Users.Remove(result);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateUser( User user)
        {
            try
            {
                using (var context = new StoreDBContext())
                {
                    context.Entry<User>(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public static User GetUserByEmailAndPassword(string email, string password)
        {
            User? user = new User();
            try
            {
                using (var context = new StoreDBContext())
                {
                    user = context.Users.SingleOrDefault(x => x.Email == email && x.Password == password);
                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return user;
        }


    }
}
