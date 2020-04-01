﻿using Ornaments.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OrnamentsWebApplication.ViewModel
{
    public class ProductViewModel
    {
        public int pageNo { get; set; }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImageURL { get; set; }

        public int CategoryId { set; get; }
        public Category Category { get; set; }
        public List<SelectListItem> CategorySelectListItems { set; get; }

        
        public List<Product> Products { set; get; }
        
        public Product Product { get; set; }
    }
}