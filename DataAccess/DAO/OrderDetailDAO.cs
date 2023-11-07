using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class OrderDetailDAO
    {
        public static List<OrderDetail> GetOrderDetails()
        {
            List<OrderDetail> orderdetails = new List<OrderDetail>();
            using (var context = new StoreDBContext())
            {
                orderdetails= context.OrderDetails.ToList();
            }
            return orderdetails;
        }

        public static OrderDetail GetOrderDetailById(int id)
        {
            OrderDetail o = new OrderDetail();
            using (var context = new StoreDBContext())
            {
                o = context.OrderDetails.SingleOrDefault(x => x.OrderDetailId == id);
            }
            return o;
        }
       

        public static void SaveOrderDetail(OrderDetail order)
        {
            try
            {
                using (var context = new StoreDBContext())
                {
                    context.OrderDetails.Add(order);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public static void DeleteOrderDetails(int id)
        {
            try
            {
                using (var context = new StoreDBContext())
                {
                    var result = context.OrderDetails.SingleOrDefault(x => x.OrderDetailId == id);
                    context.OrderDetails.Remove(result);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateOrderDetail(int id)
        {
            try
            {
                using (var context = new StoreDBContext())
                {
                    var result = context.OrderDetails.SingleOrDefault(x => x.OrderDetailId == id);
                    context.OrderDetails.Update(result);
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
