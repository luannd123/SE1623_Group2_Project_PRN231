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
    public class CategoryRepository : ICategoryRepository
    {
        public List<Category> GetCategories() => CategoryDAO.GetCategories();


        public Category GetCategoryById(int id) => CategoryDAO.GetCategory(id);

        public List<Product> GetProductsByCategoryId(int id) => CategoryDAO.GetProductByCategoryId(id);
        
    }
}
