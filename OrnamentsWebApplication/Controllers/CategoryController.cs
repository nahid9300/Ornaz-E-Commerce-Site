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
   [Authorize(Roles = "admin")]
    public class CategoryController : Controller
    {
       
        CategoryManager _categoryManager = new CategoryManager();
        // GET: Category
        [HttpGet]

        
        public ActionResult Index()
        {
            return View();
        }



        [HttpGet]
        public ActionResult Create()
        {
            CategoryViewModel categoryViewModel = new CategoryViewModel();
            return PartialView(categoryViewModel);
        }
        [HttpPost]
        public ActionResult Create(CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                Category category = Mapper.Map<Category>(categoryViewModel);

                _categoryManager.SaveCategory(category);
                return RedirectToAction("CategoryTable");
            }
            else
            {
                return new HttpStatusCodeResult(500);
            }
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            Category category = _categoryManager.GetById(Id);
            CategoryViewModel categoryViewModel = Mapper.Map<CategoryViewModel>(category);
            return PartialView(categoryViewModel);
        }

        [HttpPost]
        public ActionResult Edit(CategoryViewModel categoryViewModel)
        {
            Category category = Mapper.Map<Category>(categoryViewModel);
            _categoryManager.Update(category);
            return RedirectToAction("CategoryTable");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            Category category = _categoryManager.GetById(id);
            _categoryManager.Delete(id);

            return RedirectToAction("CategoryTable");
        }

        public ActionResult CategoryTable(string search, int? pageNo)
        {
            CategorySearchViewModel categorySearchViewModel = new CategorySearchViewModel();
            categorySearchViewModel.SearchTerm = search;

            pageNo = pageNo.HasValue ? pageNo.Value > 0 ? pageNo.Value : 1 : 1;

            var totalRecords = _categoryManager.GetCategoriesCount(search);
            categorySearchViewModel.Categories = _categoryManager.GetCategories(search, pageNo.Value);

            if (categorySearchViewModel.Categories != null)
            {
                categorySearchViewModel.Pager = new Pager(totalRecords, pageNo, 3);

                return PartialView("_CategoryTable", categorySearchViewModel);
            }
            else
            {
                return HttpNotFound();
            }
        }
    }
}