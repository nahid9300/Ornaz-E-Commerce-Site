using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ornaments.Model.Model
{
    public class Review
    {
        public int Id { get; set; }
        public string name { get; set; }
        public string reviewDetails { get; set; }
        public int productId { get; set; }
        public virtual Product Product { get; set; }
    }
}
