using Ornaments.Model.Model;
using Ornaments.BLL.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OrnamentsWebApplication.ViewModel;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Ornaments.Repository.Repository;

namespace OrnamentsWebApplication.Controllers
{
    [Authorize(Roles = "admin")]
    public class ProductController : Controller
    {
       

        ProductManager _productManager = new ProductManager();
        CategoryManager _categoryManager = new CategoryManager();
        ReviewManager _reviewManager=new ReviewManager();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ProductTable(string search, int? pageNo)
        {
            var pageSize = ConfigurationService.Instance.PageSize();

            ProductSearchViewModel model = new ProductSearchViewModel();
            model.SearchTerm = search;

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            var totalRecords = _productManager.GetProductsCount(search);
            model.Products = _productManager.GetProducts(search, pageNo.Value, pageSize);

            model.Pager = new Pager(totalRecords, pageNo, pageSize);

            return PartialView(model);
        }

        [HttpGet]
        public ActionResult Create()
        {
            ProductViewModel productViewModel = new ProductViewModel();

            productViewModel.CategorySelectListItems = _categoryManager.GetAll()
                                                       .Select(c => new SelectListItem()
                                                       {
                                                           Value = c.Id.ToString(),
                                                           Text = c.Name
                                                       }).ToList();

            productViewModel.Products = _productManager.GetAll();

            return PartialView(productViewModel);
        }

        [HttpPost]
        public ActionResult Create(ProductViewModel productViewModel)
        {
            Product product = Mapper.Map<Product>(productViewModel);
            _productManager.SaveProduct(product);
            
            productViewModel.Products = _productManager.GetAll();

            productViewModel.CategorySelectListItems = _categoryManager.GetAll()
                                                       .Select(c => new SelectListItem()
                                                       {
                                                           Value = c.Id.ToString(),
                                                           Text = c.Name
                                                       }).ToList();

            return RedirectToAction("ProductTable");
        }




        [HttpGet]
        public ActionResult Edit(int Id)
        {
            Product product = _productManager.GetById(Id);
            ProductViewModel productViewModel = Mapper.Map<ProductViewModel>(product);

            productViewModel.CategorySelectListItems = _categoryManager.GetAll()
                                                        .Select(c => new SelectListItem()
                                                        {
                                                            Value = c.Id.ToString(),
                                                            Text = c.Name
                                                        }).ToList();

            return PartialView(productViewModel);
        }

        [HttpPost]
        public ActionResult Edit(ProductViewModel productViewModel)
        {
            
            Product product = Mapper.Map<Product>(productViewModel);
            _productManager.Update(product);
            return RedirectToAction("ProductTable");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Product product = _productManager.GetById(id);

            _productManager.Delete(id);



            return RedirectToAction("ProductTable");
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
        [AllowAnonymous]
        public ActionResult Details(int Id)

        {
            ProductViewModel productViewModel=new ProductViewModel();
            ProductDetailViewModel productDetailViewModel=new ProductDetailViewModel();

            productViewModel.Products = _productManager.GetAll();
            productViewModel.Product = _productManager.GetById(Id);

            productViewModel.Reviews = _reviewManager.GetRelatedReviewByProduct(Id);


            return View(productViewModel);
        }

    
       

    }
}