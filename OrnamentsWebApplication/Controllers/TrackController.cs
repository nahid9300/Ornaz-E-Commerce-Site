using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Ornaments.Repository.Repository;
using OrnamentsWebApplication.ViewModel;

namespace OrnamentsWebApplication.Controllers
{
    [Authorize(Roles = "customer")]
    public class TrackController : Controller
    {
        private OrdersRepository _ordersRepository = new OrdersRepository();
        // GET: Track

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }




        [HttpGet]
        public ActionResult TrackOrder()
        {
            TrackOrderViewModel trackOrderViewModel=new TrackOrderViewModel();
            trackOrderViewModel.UserID = User.Identity.GetUserId();
            return View(trackOrderViewModel);
        }

        [HttpPost]
        public ActionResult TrackOrder(TrackOrderViewModel trackOrderViewModel)
        {
           
            var orders = _ordersRepository.GetAll();
            if (trackOrderViewModel.UserID != null)
            {
                orders = orders.Where(c => c.UserID.Contains(trackOrderViewModel.UserID)).ToList();
            }
            if (trackOrderViewModel.invoiceNumber != null)
            {
                orders = orders.Where(c => c.invoiceNumber.Contains(trackOrderViewModel.invoiceNumber)).ToList();
            }
            trackOrderViewModel.Orders = orders;
            return View(trackOrderViewModel);
        }
    }
}