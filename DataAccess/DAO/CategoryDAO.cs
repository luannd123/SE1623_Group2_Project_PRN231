using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class CategoryDAO
    {
        public static List<Category> GetCategories()
        {
            List<Category> list = new List<Category>();
            using(var context = new StoreDBContext())
            {
                list = context.Categories.ToList();
            }
            return list;
        }

        public static Category GetCategory(int id) 
        {
            Category category = new Category();
            using (var context = new StoreDBContext())
            {
                category = context.Categories.SingleOrDefault(x => x.CategoryId == id);
            }
            return category;
        }

        public static List<Product> GetProductByCategoryId(int id)
        {
            List<Product> list = new List<Product>();
            using(var context = new StoreDBContext())
            {
                list = context.Products.Where(x => x.CategoryId == id).ToList();
            }
            return list;
        }
    }
}
