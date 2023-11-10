using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
     public interface IOrderRepository
    {
        List<Order> GetOrders();
        OrderDetail GetOrderDetail(int id);
        Order GetOrder(int id);
        List<Order> GetOrderByDate(DateTime From , DateTime To);
        void SaveOrder(Order order);
        void DeleteOrder(int id);
        void UpdateOrder(Order order);
    }
}
