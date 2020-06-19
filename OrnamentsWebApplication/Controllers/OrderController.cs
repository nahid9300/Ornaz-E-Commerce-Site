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
    [Authorize(Roles = "admin")]
    public class OrderController : Controller
    {

        OrdersRepository _ordersRepository = new OrdersRepository();
        

        OrdersViewModel ordersViewModel = new OrdersViewModel();

        OrderDetailsViewModel orderDetailsViewModel=new OrderDetailsViewModel();


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

        // GET: Order
        public ActionResult Index(string userID, string status, int? pageNo)
        {

            ordersViewModel.UserID = userID;
            ordersViewModel.Status = status;

            var pageSize = ConfigurationService.Instance.PageSize();
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;
            ordersViewModel.Orders = _ordersRepository.SearchOrders(userID, status, pageNo.Value,pageSize);
            var totalRecords=_ordersRepository.SearchOrdersCount(userID, status);


            ordersViewModel.Pager = new Pager(totalRecords, pageNo, pageSize);
            return View(ordersViewModel);
        }

        public ActionResult Details(int ID)
        {

            orderDetailsViewModel.Order = _ordersRepository.GetOrderByID(ID);
          
            if (orderDetailsViewModel.Order != null)
            {
                orderDetailsViewModel.OrderBy = UserManager.FindById(orderDetailsViewModel.Order.UserID);
            }
            
            orderDetailsViewModel.AvailableStatuses = new List<string>() { "Pending", "In Progress", "Delivered" };

            return View(orderDetailsViewModel);
        }

        public JsonResult ChangeStatus(string status, int ID, int productID, int Quantity)
        {
            JsonResult result = new JsonResult();
            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            result.Data = new { Success = _ordersRepository.UpdateOrderStatus(ID, status, productID, Quantity) };

            return result;
        }



       
    }
}