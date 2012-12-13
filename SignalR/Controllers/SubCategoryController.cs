using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using muagicungban.Abstract;
using muagicungban.Models;
using muagicungban.Repositories;
using muagicungban.Entities;

namespace muagicungban.Controllers
{
    public class SubCategoryController : Controller
    {
        private ISubCategoriesRepository subCategoriesRepository;
        private MembersRepository membersRepository;

        public SubCategoryController()
        {
            subCategoriesRepository = new SubCategoriesRepository(Connection.connectionString);
            membersRepository = new MembersRepository(Connection.connectionString);
        }
        //
        // GET: /SubCategory/

        //public ActionResult Index()
        //{
        //    return View();
        //}
        [Authorize]
        public ViewResult List()
        {
            User user = membersRepository.Members.Single(m => m.Username == HttpContext.User.Identity.Name);
            if (user.Roles.Any(r => r.Role.RoleName == "Admin" || r.Role.RoleName == "Manager"))
            {
                return View(subCategoriesRepository.subCategories.ToList());
            }
            return View();
        }

        //
        // GET: /SubCategory/Details/5

        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //
        // GET: /SubCategory/Create

        //public ActionResult Create()
        //{
        //    ICategoriesRepository categoriesRepository = new CategoriesRepository(Connection.connectionString);
        //    ViewData["category"] = new SelectList(categoriesRepository.Categories.ToList(), "CategoryID", "CategoryName");
        //    return View();
        //} 

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
        [Authorize]
        public ActionResult Create(string catName)
        {
            User user = membersRepository.Members.Single(m => m.Username == HttpContext.User.Identity.Name);
            if (user.Roles.Any(r => r.Role.RoleName == "Admin" || r.Role.RoleName == "Manager"))
            {
                SubCategory subCategory = new SubCategory();
                //subCategory.ID = int.Parse(formCollection["ID"]);
                subCategory.Name = catName;
                subCategory.CategoryID = 1;
                subCategoriesRepository.Add(subCategory);
                return RedirectToAction("List");
            }
            return Content("Thất bại");
        }
        
        //
        // GET: /SubCategory/Edit/5
 
        //public ActionResult Edit(int id)
        //{
        //    SubCategory subCategory = subCategoriesRepository.subCategories.Single(s => s.ID == id);
        //    ICategoriesRepository categoriesRepository = new CategoriesRepository(Connection.connectionString);
        //    ViewData["category"] = new SelectList(categoriesRepository.Categories.ToList(), "CategoryID", "CategoryName", subCategory.Category);
        //    return View(subCategory);
        //}

        //
        // POST: /SubCategory/Edit/5

        [Authorize]
        [HttpPost]
        public ActionResult Edit(int id, string catName)
        {
            try
            {
                // TODO: Add update logic here
                User user = membersRepository.Members.Single(m => m.Username == HttpContext.User.Identity.Name);
                if (user.Roles.Any(r => r.Role.RoleName == "Admin" || r.Role.RoleName == "Manager"))
                {
                    SubCategory subCategory = new SubCategory();
                    subCategory.ID = id;
                    subCategory.Name = catName;
                    subCategory.CategoryID = 1;
                    subCategoriesRepository.Save(subCategory);
                    return Content(subCategory.Name);
                }
                return Content("Thất bại");
            }
            catch
            {
                return Content("");
            }
        }

        //
        // GET: /SubCategory/Delete/5
        [Authorize]
        public ActionResult Delete(int ID)
        {
            User user = membersRepository.Members.Single(m => m.Username == HttpContext.User.Identity.Name);
            if (user.Roles.Any(r => r.Role.RoleName == "Admin" || r.Role.RoleName == "Manager"))
            {
                var subCategory = subCategoriesRepository.subCategories.First(s => s.ID == ID);
                subCategoriesRepository.Delete(subCategory);
                return RedirectToAction("List");
            }
            return Content("Thất bại");
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
