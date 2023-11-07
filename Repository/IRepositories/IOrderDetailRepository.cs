using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IOrderDetailRepository
    {
        List<OrderDetail> GetOrderDetails();
        OrderDetail GetOrderById(int id);
        void SaveOrderDetail (OrderDetail orderDetail);
        void DeleteOrderDetail (int id);
        void UpdateOrderDetail (int id);
    }
}
