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

        public List<Product> GetProducts(List<int> Ids)
        {

            return _dbContext.Products.Where(product => Ids.Contains(product.Id)).ToList();
        }

        public List<Product> GetAllProductsForPagination(int pageNo)
        {
            int pageSize = 5;
            return _dbContext.Products.OrderByDescending(x => x.Id).Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();
        }


        public List<Product> GetLatesProducts(int numberOfProducts)
        {
            return _dbContext.Products.OrderByDescending(x=>x.Id).Take(numberOfProducts).Include(x=>x.Category).ToList();
        }

        public List<Product> GetAllProductsForWidget(int pageNo, int pageSize)
        {
           
            return _dbContext.Products.OrderByDescending(x => x.Id).Skip((pageNo - 1) * pageSize).Take(pageSize).Include(x=>x.Category).ToList();
        }

        public List<Product> GetRelatedProductByCategory(int categoryID, int pageSize)
        {

            return _dbContext.Products.Where(x=>x.Category.Id==categoryID).OrderByDescending(x => x.Id).Take(pageSize).Include(x => x.Category).ToList();
        }


        public int GetMaximumPrice()
        {
            return (int)(_dbContext.Products.Max(x => x.Price));
            
        }

        public List<Product> SearchProduct(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy,int pageNo, int pageSize)
        {
            var products = _dbContext.Products.ToList();

            if (categoryID.HasValue)
            {
                products=products.Where(x=>x.Category.Id==categoryID.Value).ToList();
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = products.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
            }

            if (minimumPrice.HasValue)
            {
                products = products.Where(x => x.Price >= minimumPrice.Value).ToList();
            }

            if (maximumPrice.HasValue)
            {
                products = products.Where(x => x.Price <= maximumPrice.Value).ToList();
            }

            if (sortBy.HasValue)
            {
                switch (sortBy.Value)
                {
                   
                    case 2:
                        products = products.OrderByDescending(x => x.Id).ToList();
                        break;
                    case 3:
                        products = products.OrderBy(x => x.Price).ToList();
                        break;
                    case 4:
                        products = products.OrderByDescending(x => x.Price).ToList();
                        break;
                    default:
                        products = products.OrderByDescending(x => x.Id).ToList();
                        break;
                }
            }

            return products.Skip((pageNo-1)*pageSize).Take(pageSize).ToList();
        }


        public int SearchProductCountForPagination(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy)
        {
            var products = _dbContext.Products.ToList();

            if (categoryID.HasValue)
            {
                products = products.Where(x => x.Category.Id == categoryID.Value).ToList();
            }

            if (!string.IsNullOrEmpty(searchTerm))
            {
                products = products.Where(x => x.Name.ToLower().Contains(searchTerm.ToLower())).ToList();
            }

            if (minimumPrice.HasValue)
            {
                products = products.Where(x => x.Price >= minimumPrice.Value).ToList();
            }

            if (maximumPrice.HasValue)
            {
                products = products.Where(x => x.Price <= maximumPrice.Value).ToList();
            }

            if (sortBy.HasValue)
            {
                switch (sortBy.Value)
                {

                    case 2:
                        products = products.OrderByDescending(x => x.Id).ToList();
                        break;
                    case 3:
                        products = products.OrderBy(x => x.Price).ToList();
                        break;
                    case 4:
                        products = products.OrderByDescending(x => x.Price).ToList();
                        break;
                    default:
                        products = products.OrderByDescending(x => x.Id).ToList();
                        break;
                }
            }

            return products.Count;
        }
    }
}
