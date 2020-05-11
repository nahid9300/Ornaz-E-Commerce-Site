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
    
    public class CategoryController : Controller
    {
        string message = "";
        CategoryManager _categoryManager = new CategoryManager();
        // GET: Category
        [HttpGet]
        public ActionResult Create()
        {
            CategoryViewModel categoryViewModel = new CategoryViewModel();
            categoryViewModel.Categories = _categoryManager.GetAll();
            return View(categoryViewModel);
        }
        [HttpPost]
        public ActionResult Create(CategoryViewModel categoryViewModel)
        {
            string message = "";
            Category category = Mapper.Map<Category>(categoryViewModel);
            if (_categoryManager.SaveCategory(category))
            {
                message = "Category created successfully";
            }
            else
            {
                message = "Category did not created";
            }
            ViewBag.Message = message;
            categoryViewModel.Categories = _categoryManager.GetAll();

            return View(categoryViewModel);
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            Category category = _categoryManager.GetById(Id);
            CategoryViewModel categoryViewModel = Mapper.Map<CategoryViewModel>(category);


            categoryViewModel.Categories = _categoryManager.GetAll();


            return View(categoryViewModel);
        }

        [HttpPost]
        public ActionResult Edit(CategoryViewModel categoryViewModel)
        {
            string message = "";
            Category category = Mapper.Map<Category>(categoryViewModel);
            if (_categoryManager.Update(category))
            {
                message = "Updated";
            }
            else
            {
                message = "Not Updated";
            }

            ViewBag.Message = message;

            categoryViewModel.Categories = _categoryManager.GetAll();
            return View(categoryViewModel);
        }

        [HttpGet]
        public ActionResult Delete(int id)
        {
            Category category = _categoryManager.GetById(id);
            string message = "";
            if (_categoryManager.Delete(id))
            {
                message = "Delete Succsessfully!!";
            }
            else
            {
                message = "Not delete ";
            }
            ViewBag.Message = message;

            return RedirectToAction("Create");
        }

        public ActionResult ShowAllCategory(string search, int? pageNo)
        {
            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo : 1 : 1;

            CategoryViewModel categoryViewModel = new CategoryViewModel();
            categoryViewModel.pageNo = pageNo.HasValue ? pageNo.Value : 1;

            categoryViewModel.Categories = _categoryManager.GetAllCategoriesForPagination(categoryViewModel.pageNo);

            return View(categoryViewModel);
        }
    }
}