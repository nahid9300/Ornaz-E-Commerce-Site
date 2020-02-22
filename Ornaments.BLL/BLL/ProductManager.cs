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
    }
}
