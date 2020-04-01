using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Ornaments.Model.Model;

namespace OrnamentsWebApplication.ViewModel
{
    public class ConfigViewModel
    {

        [Key]
        public string Key { get; set; }
        public string Value { get; set; }
        public List<Config> Configs { get; set; }
    }
}