using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ornaments.Model.Model;
using Ornaments.Repository.Repository;

namespace Ornaments.BLL.BLL
{
    public class CategoryManager
    {
        CategoryRepository _categoryRepository = new CategoryRepository();
        public bool SaveCategory(Category category)
        {
            return _categoryRepository.SaveCategory(category);
        }

        public bool Update(Category category)
        {
            return _categoryRepository.Update(category);

        }

        public Category GetById(int Id)
        {
            return _categoryRepository.GetById(Id);
        }
        public List<Category> GetAll()
        {
            return _categoryRepository.GetAll();
        }

        public bool Delete(int id)
        {
            return _categoryRepository.Delete(id);
        }
    }
}
