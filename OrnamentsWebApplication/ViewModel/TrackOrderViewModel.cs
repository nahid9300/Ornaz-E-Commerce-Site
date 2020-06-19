using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Ornaments.Model.Model;
using OrnamentsWebApplication.Models;

namespace OrnamentsWebApplication.ViewModel
{
    public class TrackOrderViewModel
    {
        public List<Order> Orders { get; set; }
        public string UserID { get; set; }
        public string invoiceNumber { get; set; }
        public ApplicationUser User { get; set; }
    }
}