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
    public class OrderDetailRepository : IOrderDetailRepository
    {
        public void DeleteOrderDetail(int id) => OrderDetailDAO.DeleteOrderDetails(id);
        

        public OrderDetail GetOrderById(int id) => OrderDetailDAO.GetOrderDetailById(id);
        

        public List<OrderDetail> GetOrderDetails() => OrderDetailDAO.GetOrderDetails();
        

        public void SaveOrderDetail(OrderDetail orderDetail) => OrderDetailDAO.SaveOrderDetail(orderDetail);
        

        public void UpdateOrderDetail(int id) => OrderDetailDAO.UpdateOrderDetail(id);
       
    }
}
