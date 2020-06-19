using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Ornaments.BLL.BLL;
using OrnamentsWebApplication.ViewModel;

namespace OrnamentsWebApplication.Controllers
{
    public class WidgetsController : Controller
    {
       
        public ActionResult Products(bool isLatestProducts, int? categoryID)
        {
            ProductsWidgetViewModel productsWidgetViewModel=new ProductsWidgetViewModel();
            ProductManager _productManager=new ProductManager();

            productsWidgetViewModel.IsLatestProducts = isLatestProducts;


            if(isLatestProducts)
            {
                productsWidgetViewModel.Products = _productManager.GetLatesProducts(4);
            }

            else if (categoryID.HasValue && categoryID.Value>0)
            {
                productsWidgetViewModel.Products= _productManager.GetRelatedProductByCategory(categoryID.Value,4);
            }
            else
            {
                productsWidgetViewModel.Products = _productManager.GetAllProductsForWidget(1, 8);
            }
            return PartialView(productsWidgetViewModel);
        }
    }
}