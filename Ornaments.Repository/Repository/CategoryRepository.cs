using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ornaments.DatabaseContext.DatabaseContext;
using Ornaments.Model.Model;

namespace Ornaments.Repository.Repository
{
    public class CategoryRepository
    {
        OrnamentDbContext _dbContext = new OrnamentDbContext();
        public bool SaveCategory(Category category)
        {
            _dbContext.Categories.Add(category);
            return _dbContext.SaveChanges()>0;
        }

        public bool Update(Category category)
        {
            Category aCategory = _dbContext.Categories.FirstOrDefault(c => c.Id == category.Id);
            if (aCategory != null)
            {
                aCategory.Name = category.Name;
                aCategory.Description = category.Description;
                aCategory.isFeatured = category.isFeatured;
            }

            return _dbContext.SaveChanges() > 0;
        }

        public Category GetById(int Id)
        {

            return _dbContext.Categories.FirstOrDefault((c => c.Id == Id));
        }

        public List<Category> GetAll()
        {

            return _dbContext.Categories.ToList();
        }

        public bool Delete(int id)
        {
            Category aCategory = _dbContext.Categories.FirstOrDefault(c => c.Id == id);
            _dbContext.Categories.Remove(aCategory);
            return _dbContext.SaveChanges() > 0;
        }

        public List<Category> GetFeaturedCategories()
        {

            return _dbContext.Categories.Where(c=>c.isFeatured).ToList();
        }

        public List<Category> GetAllCategoriesForPagination(int pageNo)
        {
            int pageSize = 5;
            return _dbContext.Categories.OrderByDescending(x => x.Id).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
        }

        public int GetCategoriesCount(string search)
        {
           
            
                if (!string.IsNullOrEmpty(search))
                {
                    return _dbContext.Categories.Where(category => category.Name != null &&category.Name.ToLower().Contains(search.ToLower())).Count();
                }
                else
                {
                    return _dbContext.Categories.Count();
                }
            
        }

        public List<Category> GetCategories(string search, int pageNo)
        {
            int pageSize = 3;

           
            
                if (!string.IsNullOrEmpty(search))
                {
                    return _dbContext.Categories.Where(category => category.Name != null &&
                                                                category.Name.ToLower().Contains(search.ToLower()))
                        .OrderBy(x => x.Id)
                        .Skip((pageNo - 1) * pageSize)
                        .Take(pageSize)
                        .Include(x => x.Products)
                        .ToList();
                }
                else
                {
                    return _dbContext.Categories
                        .OrderBy(x => x.Id)
                        .Skip((pageNo - 1) * pageSize)
                        .Take(pageSize)
                        .Include(x => x.Products)
                        .ToList();
                }
            
        }
    }
}
