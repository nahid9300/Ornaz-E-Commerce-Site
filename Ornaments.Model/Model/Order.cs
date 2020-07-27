using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ornaments.Model.Model
{
    public class Order
    {
        public int Id { get; set; }
        public string invoiceNumber { get; set; }
        public string TransactionNumber { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
        public DateTime OrderAt { get; set; }
        public string Status { get; set; }
        public  decimal TotalAmount { get; set; }
      
        public virtual List<OrderItem> OrderItems { get; set; }
    }
}
