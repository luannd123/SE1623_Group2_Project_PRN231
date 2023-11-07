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
    public class ProductRepository : IProductReository
    {
        public void DeleteProduct(int id) => ProductDAO.DeleteProduct(id);
       

        public List<Product> GetProductByName(string name) => ProductDAO.GetProductByName(name);
        

        public List<Product> GetProductByPrice(decimal price) => ProductDAO.GetProductByPrice(price);
        

        public List<Product> GetProducts() => ProductDAO.GetProducts();
        

        public void SaveProduct(Product product) => ProductDAO.SaveProduct(product);
        

        public void UpdateProduct(int id) => ProductDAO.UpdateProduct(id);
        
    }
}
