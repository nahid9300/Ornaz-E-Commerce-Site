using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Ornaments.Model.Model;

namespace OrnamentsWebApplication.ViewModel
{
    public class HomeViewModel
    {
        public List<Category> Categories { get; set; }
        public List<Product> Products { get; set; }

        public string Key { get; set; }
        public string Value { get; set; }
        public List<Config> Configs { get; set; }
    }
}