using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ornaments.Model.Model;

namespace OrnamentsWebApplication.ViewModel
{
    public class ProductsWidgetViewModel
    {
        public List<Product> Products { get; set; }
        public bool IsLatestProducts { get; set; }
    }
}