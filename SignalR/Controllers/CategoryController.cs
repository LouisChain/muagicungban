using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using muagicungban.Models;
using muagicungban.Abstract;
using muagicungban.Repositories;

namespace muagicungban.Controllers
{
    public class CategoryController : Controller
    {
        private ICategoriesRepository categoriesRepository;

        public CategoryController()
        {
            categoriesRepository = new CategoriesRepository(Connection.connectionString);
        }
        //
        // GET: /Category/

        public ActionResult Index()
        {
            return View();
        }

        public ViewResult List()
        {
            return View(categoriesRepository.Categories.ToList());
        }

        //
        // GET: /Category/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /Category/Create

        public ActionResult Create()
        {
            return View();
        } 

        //
        // POST: /Category/Create

        [HttpPost]
        public ActionResult Create(Category category)
        {
            try
            {
                categoriesRepository.Add(category);
                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }


        //
        // GET: /Category/Edit/5
 
        public ActionResult Edit(int ID)
        {
            var category = categoriesRepository.Categories.First(c => c.CategoryID == ID);
            return View(category);
        }

        //
        // POST: /Category/Edit/5

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                categoriesRepository.Save(category);
                TempData["message"] = category.CategoryName + " saved successful!!!";
                return RedirectToAction("list");
            }
            else
                return View(category);
        }

        //
        // GET: /Category/Delete/5
 
        public RedirectToRouteResult Delete(int ID)
        {
            var category = categoriesRepository.Categories.First(c => c.CategoryID == ID);
            categoriesRepository.Delete(category);
            TempData["message"] = category.CategoryName + " was deleted";
            return RedirectToAction("list");
        }

        //
        // POST: /Category/Delete/5


    }
}
