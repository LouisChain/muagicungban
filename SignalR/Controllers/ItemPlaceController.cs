using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using muagicungban.Entities;
using muagicungban.Repositories;

namespace muagicungban.Controllers
{
    public class ItemPlaceController : Controller
    {
        int pageSize = 20;
        MembersRepository membersRepository = new MembersRepository(Connection.connectionString);
        ItemPlaceRepository itemPlaceRepository = new ItemPlaceRepository(Connection.connectionString);
        //
        // GET: /ItemPlace/
        [Authorize]
        public ActionResult Index(string id = "current", string key = "", int page = 1)
        {
            User user = membersRepository.Members.Single(m => m.Username == HttpContext.User.Identity.Name);
            if (user.Roles.Any(r => r.Role.RoleName == "Admin" || r.Role.RoleName == "Manager"))
            {
                List<ItemPlace> items = new List<ItemPlace>();

                if (id == "pass")
                {
                    items = itemPlaceRepository.ItemPlaces.Where(i => i.EndDate < DateTime.Now && (i.Item.Title.ToLower().Contains(key.ToLower()) ||
                                                                        i.Item.Description.ToLower().Contains(key.ToLower()))).ToList();
                }
                else if (id == "current")
                {
                    items = itemPlaceRepository.ItemPlaces.Where(i => i.StartDate <= DateTime.Now && DateTime.Now < i.EndDate && (i.Item.Title.ToLower().Contains(key.ToLower()) ||
                                                                        i.Item.Description.ToLower().Contains(key.ToLower()))).ToList();
                }
                else if (id == "future")
                {
                    items = itemPlaceRepository.ItemPlaces.Where(i => i.StartDate > DateTime.Now && (i.Item.Title.ToLower().Contains(key.ToLower()) ||
                                                                        i.Item.Description.ToLower().Contains(key.ToLower()))).ToList();
                }
                else
                    items = itemPlaceRepository.ItemPlaces.Where(i => (i.Item.Title.ToLower().Contains(key.ToLower()) ||
                                                                        i.Item.Description.ToLower().Contains(key.ToLower()))).ToList();

                if (id != "")
                    ViewData["id"] = id;
                if (key != "")
                    ViewData["key"] = key;

                ViewData["pageSize"] = pageSize;
                ViewData["totalItems"] = items.Count();
                ViewData["currentPage"] = page;
                return View(items.Skip((page - 1) * pageSize).Take(pageSize).ToList());
            }
            else
                return RedirectToAction("Index", "Item");
        }

    }
}
