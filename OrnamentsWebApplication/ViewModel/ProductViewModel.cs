using Ornaments.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrnamentsWebApplication.Models;

namespace OrnamentsWebApplication.ViewModel
{
    public class ProductViewModel
    {
        public int pageNo { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ImageURL { get; set; }

        public int CategoryId { set; get; }
        public Category Category { get; set; }
        public List<SelectListItem> CategorySelectListItems { set; get; }

        
        public List<Product> Products { set; get; }
        
        public Product Product { get; set; }

        public List<Review> Reviews { get; set; }


    }
    public class ProductSearchViewModel
    {
        public List<Product> Products { get; set; }
        public string SearchTerm { get; set; }

        public Pager Pager { get; set; }
    }

    

    
}