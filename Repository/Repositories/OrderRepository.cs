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
    public class OrderRepository : IOrderRepository
    {
        public void DeleteOrder(int id) => OrderDAO.DeleteOrder(id);

        public Order GetOrder(int id) => OrderDAO.GetOrderById(id);
        

        public List<Order> GetOrderByDate(DateTime From, DateTime To) => OrderDAO.GetProductByDate(From, To);

        public OrderDetail GetOrderDetail(int id) => OrderDAO.GetOrderDetailByOrderId(id);
        

        public List<Order> GetOrders() => OrderDAO.GetOrders();
        

        public void SaveOrder(Order order) => OrderDAO.SaveOrder(order);
        

        public void UpdateOrder(Order order) => OrderDAO.UpdateOrder(order);
       
    }
}
