using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Ornaments.BLL.BLL;
using Ornaments.Model.Model;
using Ornaments.Repository.Repository;
using OrnamentsWebApplication.ViewModel;

namespace OrnamentsWebApplication.Controllers
{

    public class ShopController : Controller
    {

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

        // GET: Shop
        ProductRepository _productRepository = new ProductRepository();

        CategoryManager _categoryManager = new CategoryManager();

        private ShopRepository _shopRepository = new ShopRepository();


        [Authorize]
        public ActionResult Checkout()
        {
            CheckoutViewModel checkoutViewModel = new CheckoutViewModel();
            var cartProductsCookie = Request.Cookies["CartProducts"];

            if (cartProductsCookie != null && !string.IsNullOrEmpty(cartProductsCookie.Value))
            {
                //var productIDs = cartProductsCookie.Value;

                //var ids = productIDs.Split('-');

                //List<int> pIDs = ids.Select(x => int.Parse(x)).ToList();

                checkoutViewModel.CartProductsIds = cartProductsCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();

                checkoutViewModel.CartProducts = _productRepository.GetProducts(checkoutViewModel.CartProductsIds);
                checkoutViewModel.User = UserManager.FindById(User.Identity.GetUserId());

            }
            return View(checkoutViewModel);
        }

        public ActionResult Index(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy, int? pageNo)
        {
            var pageSize = ConfigurationService.Instance.ShopPageSize();

            ShopViewModel shopViewModel = new ShopViewModel();

            shopViewModel.SearchTerm = searchTerm;
            shopViewModel.FeaturedCategories = _categoryManager.GetFeaturedCategories();
            shopViewModel.MaximumPrice = _productRepository.GetMaximumPrice();

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            shopViewModel.SortBy = sortBy;
            shopViewModel.CategoryId = categoryID;

            int totalCount = _productRepository.SearchProductCountForPagination(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy);
            shopViewModel.Products = _productRepository.SearchProduct(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy, pageNo.Value, pageSize);

            //pagination

            shopViewModel.Pager = new Pager(totalCount, pageNo, pageSize);

            return View(shopViewModel);
        }

        public ActionResult FilterProductsByPrice(string searchTerm, int? minimumPrice, int? maximumPrice, int? categoryID, int? sortBy, int? pageNo)
        {
            var pageSize = ConfigurationService.Instance.ShopPageSize();
            PriceFilteringViewModel priceFilteringViewModel = new PriceFilteringViewModel();


            priceFilteringViewModel.SearchTerm = searchTerm;
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            priceFilteringViewModel.SortBy = sortBy;
            priceFilteringViewModel.CategoryId = categoryID;

            int totalCount = _productRepository.SearchProductCountForPagination(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy);
            priceFilteringViewModel.Products = _productRepository.SearchProduct(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy, pageNo.Value, pageSize);

            priceFilteringViewModel.Pager = new Pager(totalCount, pageNo, pageSize);

            return PartialView(priceFilteringViewModel);
        }

        public JsonResult PlaceOrder(string productIDs)
        {

            JsonResult result = new JsonResult();

            result.JsonRequestBehavior = JsonRequestBehavior.AllowGet;

            if (!string.IsNullOrEmpty(productIDs))
            {
                var productQuantities = productIDs.Split('-').Select(x => int.Parse(x)).ToList();

                var boughtProducts = _productRepository.GetProducts(productQuantities.Distinct().ToList());

                Order newOrder = new Order();
                newOrder.UserID = User.Identity.GetUserId();
                newOrder.OrderAt = DateTime.Now;
                newOrder.Status = "Pending";

                newOrder.OrderItems = new List<OrderItem>();
                newOrder.OrderItems.AddRange(boughtProducts.Select(x => new OrderItem()
                { ProductID = x.Id, Quantity = productQuantities.Where(productID => productID == x.Id).Count() }));

                newOrder.TotalAmount = boughtProducts.Sum(x =>
                    x.Price * productQuantities.Where(productID => productID == x.Id).Count());

                var rowsEffected = _shopRepository.SaveOrder(newOrder);
                result.Data = new { Success = true, Rows = rowsEffected };
            }
            else
            {
                result.Data = new { Success = false };
            }

            return result;
        }
    }
}