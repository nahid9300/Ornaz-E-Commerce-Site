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
    public class OrdersRepository
    {
        OrnamentDbContext _dbContext = new OrnamentDbContext();

        public List<Order> SearchOrders(string userID, string status, int pageNo, int pageSize)
        {
            var orders = _dbContext.Orders.ToList();

            if (!string.IsNullOrEmpty(userID))
            {
                orders = orders.Where(x => x.UserID.ToLower().Contains(userID.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(status))
            {
                orders = orders.Where(x => x.Status.ToLower().Contains(status.ToLower())).ToList();
            }

            return orders.Skip((pageNo - 1) * pageSize).Take(pageSize).ToList();

        }

        public int SearchOrdersCount(string userID, string status)
        {
            var orders = _dbContext.Orders.ToList();

            if (!string.IsNullOrEmpty(userID))
            {
                orders = orders.Where(x => x.UserID.ToLower().Contains(userID.ToLower())).ToList();
            }

            if (!string.IsNullOrEmpty(status))
            {
                orders = orders.Where(x => x.Status.ToLower().Contains(status.ToLower())).ToList();
            }

            return orders.Count;
        }

        public Order GetOrderByID(int ID)
        {
            return _dbContext.Orders.Where(x => x.Id == ID).Include(x => x.OrderItems).Include("OrderItems.Product").FirstOrDefault();

        }

        public bool UpdateOrderStatus(int ID, string status, int productID, int Quantity)
        {
            var aOrderStatus = _dbContext.Orders.FirstOrDefault(c => c.Id ==ID);
            aOrderStatus.Status = status;

            if (status.Equals("Delivered"))
            {
                var product = _dbContext.Products.FirstOrDefault(p => p.Id == productID);
                product.Quantity = product.Quantity - Quantity;
            }

            return _dbContext.SaveChanges() > 0;

        }

        public Order GetTotalByUserID(string userid)
        {
            return _dbContext.Orders.Where(x => x.UserID == userid).Include(x => x.TotalAmount).FirstOrDefault();

        }

        public List<Order> GetAll()
        {

            return _dbContext.Orders.ToList();
        }

    }
}
