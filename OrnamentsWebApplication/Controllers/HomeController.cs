using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrnamentsWebApplication.ViewModel;
using Ornaments.BLL.BLL;

namespace OrnamentsWebApplication.Controllers
{
    public class HomeController : Controller
    {
        CategoryManager _categoryManager=new CategoryManager();
        public ActionResult Index()
        {
            HomeViewModel homeViewModel=new HomeViewModel();
            homeViewModel.Categories = _categoryManager.GetAll();
            return View(homeViewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}