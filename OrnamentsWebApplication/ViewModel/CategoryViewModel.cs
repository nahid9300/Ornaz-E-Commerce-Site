using Ornaments.Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrnamentsWebApplication.ViewModel
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public bool isFeatured { get; set; }
        public int pageNo { get; set; }
        public List<Category> Categories { get; set; }
    }
}