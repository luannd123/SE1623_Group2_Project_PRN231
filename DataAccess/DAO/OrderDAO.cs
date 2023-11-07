using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class OrderDAO
    {
        public static List<Order> GetOrders()
        {
            List<Order> orders = new List<Order>();
            using (var context = new StoreDBContext())
            {
                orders = context.Orders.ToList();
            }
            return orders;
        }

        public static List<Order> GetProductByDate(DateTime From , DateTime To)
        {
            List<Order> orders = new List<Order>();
            using (var context = new StoreDBContext())
            {
                orders = context.Orders.Where(x => From <= x.OrderDate && x.OrderDate <= To).ToList();
            }
            return orders;
        }

        public static void SaveOrder(Order order)
        {
            try
            {
                using (var context = new StoreDBContext())
                {
                    context.Orders.Add(order);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public static void DeleteOrder(int id)
        {
            try
            {
                using (var context = new StoreDBContext())
                {
                    var result = context.Orders.SingleOrDefault(x => x.OrderId == id);
                    context.Orders.Remove(result);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateOrder(int id)
        {
            try
            {
                using (var context = new StoreDBContext())
                {
                    var result = context.Orders.SingleOrDefault(x => x.OrderId == id);
                    context.Orders.Update(result);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
