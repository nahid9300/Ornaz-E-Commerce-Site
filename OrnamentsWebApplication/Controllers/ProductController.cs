using Ornaments.Model.Model;
using Ornaments.BLL.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrnamentsWebApplication.ViewModel;
using AutoMapper;

namespace OrnamentsWebApplication.Controllers
{
    public class ProductController : Controller
    {
        string message = "";

        ProductManager _productManager = new ProductManager();
        CategoryManager _categoryManager = new CategoryManager();


        [HttpGet]
        public ActionResult Add()
        {
            ProductViewModel productViewModel = new ProductViewModel();

            productViewModel.CategorySelectListItems = _categoryManager.GetAll()
                                                       .Select(c => new SelectListItem()
                                                       {
                                                           Value = c.Id.ToString(),
                                                           Text = c.Name
                                                       }).ToList();

            productViewModel.Products = _productManager.GetAll();

            return View(productViewModel);
        }

        [HttpPost]
        public ActionResult Add(ProductViewModel productViewModel)
        {
            string message = "";


            Product product = Mapper.Map<Product>(productViewModel);
            if (_productManager.SaveProduct(product))
            {
                message = "saved";
            }
            else
            {
                message = "not saved";
            }

            ViewBag.message = message;

            productViewModel.Products = _productManager.GetAll();

            productViewModel.CategorySelectListItems = _categoryManager.GetAll()
                                                       .Select(c => new SelectListItem()
                                                       {
                                                           Value = c.Id.ToString(),
                                                           Text = c.Name
                                                       }).ToList();

            return View(productViewModel);
        }




        [HttpGet]
        public ActionResult Edit(int Id)
        {
            Product product = _productManager.GetById(Id);
            ProductViewModel productViewModel = Mapper.Map<ProductViewModel>(product);


            productViewModel.Products = _productManager.GetAll();


            return View(productViewModel);
        }

        [HttpPost]
        public ActionResult Edit(ProductViewModel productViewModel)
        {
            string message = "";
            Product product = Mapper.Map<Product>(productViewModel);
            if (_productManager.Update(product))
            {
                message = "Updated";
            }
            else
            {
                message = "Not Updated";
            }

            ViewBag.Message = message;

            productViewModel.Products = _productManager.GetAll();
            return View(productViewModel);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Product product = _productManager.GetById(id);
            string message = "";
            if (_productManager.Delete(id))
            {
                message = "Delete Succsessfully!!";
            }
            else
            {
                message = "Not delete ";
            }
            ViewBag.Message = message;

            return RedirectToAction("Add");
        }

       
        public ActionResult ShowAllProduct(string search, int? pageNo)
        {
            pageNo = pageNo.HasValue ? pageNo.Value>0? pageNo : 1: 1;


            ProductViewModel productViewModel = new ProductViewModel();

            productViewModel.pageNo = pageNo.HasValue ? pageNo.Value : 1;
            productViewModel.Products = _productManager.GetAllProductsForPagination(productViewModel.pageNo);

            return PartialView(productViewModel);
        }

        [HttpGet]
        public ActionResult Details(int Id)

        {
            ProductViewModel productViewModel=new ProductViewModel();
            ProductDetailViewModel productDetailViewModel=new ProductDetailViewModel();

            productViewModel.Products = _productManager.GetAll();
            productViewModel.Product = _productManager.GetById(Id);

            


            return View(productViewModel);
        }

    }
}