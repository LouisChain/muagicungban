using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using muagicungban.Entities;
using muagicungban.Repositories;

namespace muagicungban.Controllers
{
    public class ShowablePlaceController : Controller
    {
        MembersRepository membersRepository;
        ShowablePlaceRepository showablePlacesRepository;

        public ShowablePlaceController()
        {
            membersRepository = new MembersRepository(Connection.connectionString);
            showablePlacesRepository = new ShowablePlaceRepository(Connection.connectionString);
        }
        //
        // GET: /ShowablePlace/

        public ActionResult Index()
        {
            return View(showablePlacesRepository.ShowablePlaces.ToList());
        }

        [Authorize]
        public ActionResult List()
        {
            User user = membersRepository.Members.Single(m => m.Username == HttpContext.User.Identity.Name);
            if (user.Roles.Any(r => r.Role.RoleName == "Admin" || r.Role.RoleName == "Manager"))
            {
                return View(showablePlacesRepository.ShowablePlaces.ToList());
            }
            else
                return View("Index", showablePlacesRepository.ShowablePlaces.ToList());
        }

        [Authorize]
        public ActionResult UpdatePrice(string id, string price)
        {
            User user = membersRepository.Members.Single(m => m.Username == HttpContext.User.Identity.Name);
            if (user.Roles.Any(r => r.Role.RoleName == "Admin" || r.Role.RoleName == "Manager"))
            {
                if (showablePlacesRepository.ShowablePlaces.Any(s => s.PlaceName == id))
                {
                    ShowablePlace item = showablePlacesRepository.ShowablePlaces.Single(s => s.PlaceName == id);
                    item.PricePerDay = decimal.Parse(price);
                    showablePlacesRepository.Save(item);
                    return Content("OK");
                }
                return Content("Thất bại");
            }
            return Content("Cập nhật thất bại!");
        }

    }
}
