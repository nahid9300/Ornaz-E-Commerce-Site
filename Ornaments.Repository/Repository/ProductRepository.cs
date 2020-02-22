using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ornaments.DatabaseContext.DatabaseContext;
using Ornaments.Model.Model;

namespace Ornaments.Repository.Repository
{
    public class ProductRepository
    {
        OrnamentDbContext _dbContext = new OrnamentDbContext();
        public bool SaveProduct(Product product)
        {
            _dbContext.Products.Add(product);
            return _dbContext.SaveChanges()>0;
        }

        public bool Update(Product product)
        {
            Product aProduct = _dbContext.Products.FirstOrDefault(c => c.Id == product.Id);
            if (aProduct != null)
            {
                aProduct.Name = product.Name;
                aProduct.Description = product.Description;
            }

            return _dbContext.SaveChanges() > 0;
        }

        public Product GetById(int Id)
        {

            return _dbContext.Products.FirstOrDefault((c => c.Id == Id));
        }

        public List<Product> GetAll()
        {

            return _dbContext.Products.ToList();
        }

        public bool Delete(int id)
        {
            Product aProduct = _dbContext.Products.FirstOrDefault(c => c.Id == id);
            _dbContext.Products.Remove(aProduct);
            return _dbContext.SaveChanges() > 0;
        }
    }
}
