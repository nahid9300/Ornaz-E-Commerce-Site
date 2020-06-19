using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ornaments.DatabaseContext.DatabaseContext;
using Ornaments.Model.Model;

namespace Ornaments.Repository.Repository
{
    public class ShopRepository
    {
        OrnamentDbContext _dbContext = new OrnamentDbContext();
       
        public bool SaveOrder(Order order)
        {
            
            _dbContext.Orders.Add(order);

            return _dbContext.SaveChanges() > 0;
        }
    }
}
