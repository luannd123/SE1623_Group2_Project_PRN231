using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.IRepositories
{
    public interface IProductReository
    {
        List<Product> GetProducts();
        Product GetProductById(int id);
        List<Product> GetProductByName(string name);
        List<Product> GetProductByPrice(decimal price);
        void SaveProduct (Product product);
        void DeleteProduct (int id);
        void UpdateProduct (int id , Product product);

       
    }
}
