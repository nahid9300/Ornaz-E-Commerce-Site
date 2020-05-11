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

        public ActionResult Index(string  searchTerm, int? minimumPrice,int? maximumPrice,int? categoryID, int? sortBy,int? pageNo)
        {
            var pageSize = ConfigurationService.Instance.ShopPageSize();

            ShopViewModel shopViewModel=new ShopViewModel();

            shopViewModel.SearchTerm = searchTerm;
            shopViewModel.FeaturedCategories = _categoryManager.GetFeaturedCategories();
            shopViewModel.MaximumPrice = _productRepository.GetMaximumPrice();

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            shopViewModel.SortBy = sortBy;
            shopViewModel.CategoryId = categoryID;

            int totalCount = _productRepository.SearchProductCountForPagination(searchTerm, minimumPrice, maximumPrice, categoryID, sortBy);
            shopViewModel.Products = _productRepository.SearchProduct(searchTerm, minimumPrice, maximumPrice, categoryID,sortBy,pageNo.Value, pageSize);

            //pagination
           
            shopViewModel.Pager=new Pager(totalCount,pageNo, pageSize);

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

    }
}