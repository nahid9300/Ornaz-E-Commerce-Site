using Ornaments.Model.Model;
using Ornaments.Repository.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ornaments.BLL.BLL
{
    public class ProductManager
    {
        ProductRepository _productRepository = new ProductRepository();

        public bool SaveProduct(Product product)
        {
            return _productRepository.SaveProduct(product);
        }

        public List<Product> GetAll()
        {

            return _productRepository.GetAll();
        }

        public bool Update(Product product)
        {
            return _productRepository.Update(product);
        }

        public Product GetById(int Id)

        {
            return _productRepository.GetById(Id);

        }
        public bool Delete(int id)
        {
            return _productRepository.Delete(id);
        }

        public List<Product> GetAllProductsForPagination(int pageNo)
        {
            return _productRepository.GetAllProductsForPagination(pageNo);
        }

        public List<Product> GetLatesProducts(int numberOfProducts)
        {
            return _productRepository.GetLatesProducts(numberOfProducts); 
        }

        public List<Product> GetAllProductsForWidget(int pageNo, int pageSize)
        {
            return _productRepository.GetAllProductsForWidget(pageNo, pageSize);
        }

        public List<Product> GetRelatedProductByCategory(int categoryID, int pageSize)
        {
            return _productRepository.GetRelatedProductByCategory(categoryID, pageSize);
        }

        public int GetProductsCount(string search)
        {
            return _productRepository.GetProductsCount(search);
        }

        public List<Product> GetProducts(string search, int pageNo, int pageSize)
        {
            return _productRepository.GetProducts(search, pageNo, pageSize);
        }

    }
}
