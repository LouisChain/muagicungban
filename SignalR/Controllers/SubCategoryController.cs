using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using muagicungban.Abstract;
using muagicungban.Models;
using muagicungban.Repositories;

namespace muagicungban.Controllers
{
    public class SubCategoryController : Controller
    {
        private ISubCategoriesRepository subCategoriesRepository;

        public SubCategoryController()
        {
            subCategoriesRepository = new SubCategoriesRepository(Connection.connectionString);
        }
        //
        // GET: /SubCategory/

        public ActionResult Index()
        {
            return View();
        }

        public ViewResult List()
        {
            return View(subCategoriesRepository.subCategories.ToList());
        }

        //
        // GET: /SubCategory/Details/5

        public ActionResult Details(int id)
        {
            return View();
        }

        //
        // GET: /SubCategory/Create

        public ActionResult Create()
        {
            ICategoriesRepository categoriesRepository = new CategoriesRepository(Connection.connectionString);
            ViewData["category"] = new SelectList(categoriesRepository.Categories.ToList(), "CategoryID", "CategoryName");
            return View();
        } 

        //
        // POST: /SubCategory/Create
        /*
        [HttpPost]
        public ActionResult Create(SubCategory subCategory)
        {
            try
            {
                // TODO: Add insert logic here
                subCategoriesRepository.Add(subCategory);
                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }
        */

        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            SubCategory subCategory = new SubCategory();
            //subCategory.ID = int.Parse(formCollection["ID"]);
            subCategory.Name = formCollection["Name"];
            subCategory.CategoryID = int.Parse(formCollection["CategoryID"]);
            subCategoriesRepository.Add(subCategory);
            return RedirectToAction("List");
        }
        
        //
        // GET: /SubCategory/Edit/5
 
        public ActionResult Edit(int ID)
        {
            SubCategory subCategory = subCategoriesRepository.subCategories.Single(s => s.ID == ID);
            ICategoriesRepository categoriesRepository = new CategoriesRepository(Connection.connectionString);
            ViewData["category"] = new SelectList(categoriesRepository.Categories.ToList(), "CategoryID", "CategoryName", subCategory.Category);
            return View(subCategory);
        }

        //
        // POST: /SubCategory/Edit/5

        [HttpPost]
        public ActionResult Edit(int ID, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                SubCategory subCategory = new SubCategory();
                subCategory.ID = ID;
                subCategory.Name = collection["Name"];
                subCategory.CategoryID = int.Parse(collection["CategoryID"]);
                subCategoriesRepository.Save(subCategory);
                return RedirectToAction("List");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /SubCategory/Delete/5
 
        public RedirectToRouteResult Delete(int ID)
        {
            var subCategory = subCategoriesRepository.subCategories.First(s => s.ID == ID);
            subCategoriesRepository.Delete(subCategory);
            return RedirectToAction("List");
        }

        //
        // POST: /SubCategory/Delete/5

        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here
 
        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}
    }
}
