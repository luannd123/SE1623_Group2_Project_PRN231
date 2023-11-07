using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class ProductDAO
    {
        public static List<Product> GetProducts()
        {
            List<Product> products = new List<Product>();
            using(var context = new StoreDBContext())
            {
                products = context.Products.ToList();
            }
            return products;
        }

        public static Product GetProductById(int id)
        {
            Product p = new Product();
            using (var context = new StoreDBContext())
            {
                p = context.Products.SingleOrDefault(x => x.ProductId == id);
            }
            return p;
        }

        public static List<Product> GetProductByPrice(decimal price)
        {
            List<Product> products = new List<Product>();
            using (var context = new StoreDBContext())
            {
                products = context.Products.Where(x => x.UnitPrice == price).ToList();
            }
            return products;
        }
        public static List<Product> GetProductByName(string name) 
        {
            List<Product> products = new List<Product>();
            using (var context = new StoreDBContext())
            {
                products = context.Products.Where(x => x.ProductName.ToLower().Contains(name.ToLower())).ToList();
            }
            return products;
        }

        public static void SaveProduct(Product product)
        {
            try
            {
                using (var context = new StoreDBContext())
                {
                    context.Products.Add(product);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            
        }

        public static void DeleteProduct(int id)
        {           
            try
            {               
                using (var context = new StoreDBContext())
                {
                    var result = context.Products.SingleOrDefault(x => x.ProductId == id);
                    context.Products.Remove(result);
                    context.SaveChanges();
                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void UpdateProduct(int id)
        {
            try
            {
                using(var context = new StoreDBContext())
                {
                    var result = context.Products.SingleOrDefault(x => x.ProductId == id);
                    context.Products.Update(result);
                    context.SaveChanges();
                }
            }catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
