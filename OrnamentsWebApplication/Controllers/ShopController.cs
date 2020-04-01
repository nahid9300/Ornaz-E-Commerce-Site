using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ornaments.BLL.BLL;
using Ornaments.Repository.Repository;
using OrnamentsWebApplication.ViewModel;

namespace OrnamentsWebApplication.Controllers
{
    public class ShopController : Controller
    {
        // GET: Shop
        ProductRepository _productRepository=new ProductRepository();

        CategoryManager _categoryManager=new CategoryManager();
        public ActionResult Checkout()
        {
            CheckoutViewModel checkoutViewModel=new CheckoutViewModel();
            var cartProductsCookie = Request.Cookies["CartProducts"];

            if (cartProductsCookie != null)
            {
                //var productIDs = cartProductsCookie.Value;

                //var ids = productIDs.Split('-');

                //List<int> pIDs = ids.Select(x => int.Parse(x)).ToList();

                checkoutViewModel.CartProductsIds= cartProductsCookie.Value.Split('-').Select(x => int.Parse(x)).ToList();

                checkoutViewModel.CartProducts = _productRepository.GetProducts(checkoutViewModel.CartProductsIds);
            }
            return View(checkoutViewModel);
        }

        public ActionResult Index(string  searchTerm, int? minimumPrice,int? maximumPrice,int? categoryID, int? sortBy)
        {
            ShopViewModel shopViewModel=new ShopViewModel();

            shopViewModel.FeaturedCategories = _categoryManager.GetFeaturedCategories();
            shopViewModel.MaximumPrice = _productRepository.GetMaximumPrice();

            shopViewModel.Products = _productRepository.SearchProduct(searchTerm, minimumPrice, maximumPrice, categoryID,sortBy);

            shopViewModel.SortBy = sortBy;
            return View(shopViewModel);
        }
    }
}