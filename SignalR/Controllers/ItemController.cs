using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Linq;

using muagicungban.Entities;
using muagicungban.Models;
using muagicungban.Repositories;

namespace muagicungban.Controllers
{
    public class ItemController : Controller
    {
        public const int pageSize = 22;

        private ItemsRepository itemsRepository;
        private ImagesRepository imagesRepository;
        private CategoriesRepository categoriesRepository;
        private SubCategoriesRepository subCategoriesRepository;
        private ShipmentRepository shipmentRepository;
        private BidsRepository bidsRepository;
        private ShowablePlaceRepository showablePlaces;
        private ItemPlaceRepository itemPlaces;
        private WatchListRepository watchLists;
        private MembersRepository membersRepository;
        private PaymentHistoryRepository PaymentHistoryRepository;

        public ItemController()
        {
            itemsRepository = new ItemsRepository(Connection.connectionString);
            imagesRepository = new ImagesRepository(Connection.connectionString);
            categoriesRepository = new CategoriesRepository(Connection.connectionString);
            shipmentRepository = new ShipmentRepository(Connection.connectionString);
            bidsRepository = new BidsRepository(Connection.connectionString);
            showablePlaces = new ShowablePlaceRepository(Connection.connectionString);
            itemPlaces = new ItemPlaceRepository(Connection.connectionString);
            membersRepository = new MembersRepository(Connection.connectionString);
            subCategoriesRepository = new SubCategoriesRepository(Connection.connectionString);
            watchLists = new WatchListRepository(Connection.connectionString);
            PaymentHistoryRepository = new Repositories.PaymentHistoryRepository(Connection.connectionString);
            ViewData["categories"] = subCategoriesRepository.subCategories.ToList();

            // For showing number of uncheck item
            int i = 0;
            foreach (var item in itemsRepository.Items.Where(a => a.IsChecked == false && !a.Owner.IsBan))
                i++;
            ViewData["Uncheck"] = i;

            // For showing number of item list
            ViewData["List"] = itemsRepository.Items.Count();

            // For showing number of ended item
            ViewData["EndedNums"] = itemsRepository.Items.Where(j => j.EndDate < DateTime.Now && j.IsActive && j.IsChecked && !j.Owner.IsBan).Count();

            // For showing number of selling item
            ViewData["SellingNums"] = itemsRepository.Items.Where(j => j.StartDate <= DateTime.Now && DateTime.Now < j.EndDate && j.IsActive && j.IsChecked && !j.Owner.IsBan).Count();
            
            // For showing number of future item
            ViewData["FutureNums"] = itemsRepository.Items.Where(j => DateTime.Now < j.StartDate && j.IsActive && j.IsChecked && !j.Owner.IsBan).Count();

        }

        //
        // GET: /Item/

        public ActionResult Index(string key = "", string category = "", int page = 1)
        {
            List<Item> items = new List<Item>();
            if (key == "" && category == "")
            {
                if (itemPlaces.ItemPlaces.Any(i => i.PlaceName == "index1"))
                {
                    if (itemPlaces.ItemPlaces.Any(p => p.PlaceName == "index1" && p.StartDate <= DateTime.Now && DateTime.Now < p.EndDate))
                    {
                        ItemPlace _itemplace = itemPlaces.ItemPlaces.Where(p => p.PlaceName == "index1" && p.IsPaid && p.StartDate <= DateTime.Now && DateTime.Now < p.EndDate).First();
                        Item _item = itemsRepository.Items.Single(i => i.ItemID == _itemplace.ItemID);
                        if (_item.IsActive && _item.IsChecked && !_item.Owner.IsBan)
                            items.Add(_item);
                    }
                }

                if (itemPlaces.ItemPlaces.Any(i => i.PlaceName == "index10"))
                {
                    List<ItemPlace> _itemPlaces = itemPlaces.ItemPlaces.Where(p => p.PlaceName == "index10" && p.StartDate <= DateTime.Now && DateTime.Now < p.EndDate).ToList();
                    foreach (var item in _itemPlaces)
                    {
                        Item _item = itemsRepository.Items.First(i => i.ItemID == item.ItemID);
                        if (_item.IsChecked && _item.IsActive && !_item.Owner.IsBan)
                            if (!items.Any(x => x.ItemID == _item.ItemID))
                                items.Add(_item);
                    }
                }
                if (itemPlaces.ItemPlaces.Any(i => i.PlaceName == "index100"))
                {
                    List<ItemPlace> _itemPlaces = itemPlaces.ItemPlaces.Where(p => p.PlaceName == "index100" && p.IsPaid && p.EndDate >= DateTime.Now && p.StartDate <= DateTime.Now).ToList();
                    foreach (var item in _itemPlaces)
                    {
                        Item _item = itemsRepository.Items.First(i => i.ItemID == item.ItemID);
                        if (_item.IsChecked && _item.IsActive && !_item.Owner.IsBan)
                            if (!items.Any(x => x.ItemID == _item.ItemID))
                                items.Add(_item);
                    }
                }
            }
            else
            {
                items = itemsRepository.Items.Where(i => i.IsChecked && i.IsActive && !i.Owner.IsBan &&
                                (i.Title.ToLower().Contains(key.ToLower()) || i.Description.ToLower().Contains(key.ToLower()))).ToList();
                if (key != "")
                    ViewData["key"] = key;
                if (category != "" && category != "All")
                {
                    ViewData["category"] = category;
                    items = items.Where(i => i.SubCategoryID == int.Parse(category)).ToList();
                }
            }

            ViewData["pageSize"] = pageSize;
            ViewData["totalItems"] = items.Count();
            ViewData["currentPage"] = page;

            return View("Index", items.Skip((page - 1) * pageSize).Take(pageSize));
        }

        public ActionResult View(string id, string key = "", string category = "", int page = 1)
        {
            List<Item> items = new List<Item>();
            if (id == "ended")
            {
                items = itemsRepository.Items.Where(i => i.EndDate < DateTime.Now && i.IsActive && i.IsChecked && !i.Owner.IsBan && 
                        (i.Title.ToLower().Contains(key.ToLower()) || i.Description.ToLower().Contains(key.ToLower()))).ToList();
            }
            else if (id == "selling")
            {
                items = itemsRepository.Items.Where(i => i.StartDate <= DateTime.Now && DateTime.Now < i.EndDate && i.IsChecked && i.IsActive && !i.Owner.IsBan &&
                    (i.Title.ToLower().Contains(key.ToLower()) || i.Description.ToLower().Contains(key.ToLower()))).ToList();
            }
            else if (id == "future")
            {
                items = itemsRepository.Items.Where(i => DateTime.Now < i.StartDate && i.IsActive && i.IsChecked && !i.Owner.IsBan &&
                    (i.Title.ToLower().Contains(key.ToLower()) || i.Description.ToLower().Contains(key.ToLower()))).ToList();
            }
            else
                RedirectToAction("index");

            if (key != "")
                ViewData["key"] = key;
            if (category != "" && category != "All")
            {
                ViewData["category"] = category;
                items = items.Where(i => i.SubCategoryID == int.Parse(category)).ToList();
            }
            ViewData["pageSize"] = pageSize;
            ViewData["totalItems"] = items.Count();
            ViewData["currentPage"] = page;

            

            return View("Index", items.Skip((page - 1) * pageSize).Take(pageSize));
        }

        //public ActionResult Search(string key, string category, int page = 1)
        //{
        //    List<Item> items = new List<Item>();
        //    if (category == "All")
        //    {
        //        items = itemsRepository.Items.Where(i =>  i.IsChecked && i.IsActive &&
        //                                !i.Owner.IsBan && 
        //                                (i.Title.ToLower().Contains(key.ToLower()) || i.Description.ToLower().Contains(key.ToLower()))).ToList();
        //    }
        //    else
        //    {
        //        if (key == "")
        //            items = itemsRepository.Items.Where(i => i.SubCategoryID == int.Parse(category) && i.IsChecked && i.IsActive && !i.Owner.IsBan ).ToList();
        //        else
        //            items = itemsRepository.Items.Where(i =>  i.SubCategoryID == int.Parse(category) &&
        //                                i.IsChecked && i.IsActive &&
        //                                !i.Owner.IsBan && (i.Title.ToLower().Contains(key.ToLower()) || i.Description.ToLower().Contains(key.ToLower()))).ToList();
        //    }
        //    ViewData["key"] = key;
        //    ViewData["category"] = category;
        //    ViewData["pageSize"] = pageSize;
        //    ViewData["totalItems"] = items.Count();
        //    ViewData["currentPage"] = page;

        //    return View("Index", items.Skip((page - 1) * pageSize).Take(pageSize));
        //}

        //public ActionResult Category(int id, string type)
        //{
        //    List<Item> items;
        //    if (type != null)
        //    {
        //        // List category with ended session
        //        if (type == "1")
        //        {
        //            items = itemsRepository.Items.Where(i => i.SubCategoryID == id && i.EndDate < DateTime.Now).ToList();
        //        }
        //        else if (type == "2")
        //        {
        //            items = itemsRepository.Items.Where(i => i.SubCategoryID == id && i.StartDate < DateTime.Now && i.EndDate > DateTime.Now).ToList();
        //        }
        //        else
        //            items = itemsRepository.Items.Where(i => i.SubCategoryID == id && i.StartDate > DateTime.Now).ToList();
        //    }
        //    else
        //        items = itemsRepository.Items.Where(i => i.SubCategoryID == id).ToList();
        //    return View("Index", items);
        //}


        [Authorize]
        public ActionResult WatchList()
        {
            List<Item> items = new List<Item>();

            var watchList = watchLists.WatchLists.Where(w => w.Username == HttpContext.User.Identity.Name).ToList();
            foreach (var item in watchList)
            {
                items.Add(itemsRepository.Items.Single(i => i.ItemID == item.ItemID));
            }

            return View(items);
        }

        [Authorize]
        public ActionResult AddWatchList(long id)
        {
            WatchList watchList = new WatchList();
            watchList.ItemID = id;
            watchList.Username = HttpContext.User.Identity.Name;
            watchLists.Add(watchList);
            return RedirectToAction("WatchList");
        }

        // List all item that belong to current user /item/all?page=1
        [Authorize]
        public ActionResult List(string id, string key = "", string category = "", int page = 1)
        {
            List<Item> items;
            User user = membersRepository.Members.Single(m => m.Username == HttpContext.User.Identity.Name);
            if (id == "all")
            {
                if (user.Roles.Any(r => r.Role.RoleName == "Admin" || r.Role.RoleName == "Manager"))
                {
                    items = itemsRepository.Items.ToList();
                }
                else
                    items = itemsRepository.Items.Where(i => i.OwnerID == user.Username).ToList();
            }
            else if (id == "future")
            {
                if (user.Roles.Any(r => r.Role.RoleName == "Admin" || r.Role.RoleName == "Manager"))
                    items = itemsRepository.Items.Where(i => DateTime.Now < i.StartDate && i.IsChecked).ToList();
                else
                    items = itemsRepository.Items.Where(i => i.OwnerID == user.Username && DateTime.Now < i.StartDate && i.IsChecked).ToList();
            }
            else if (id == "current")
            {
                if (user.Roles.Any(r => r.Role.RoleName == "Admin" || r.Role.RoleName == "Manager"))
                    items = itemsRepository.Items.Where(i => i.StartDate <= DateTime.Now &&
                                                            DateTime.Now < i.EndDate && i.IsChecked).ToList();
                else
                    items = itemsRepository.Items.Where(i => i.OwnerID == user.Username &&
                                            i.StartDate <= DateTime.Now &&
                                            DateTime.Now < i.EndDate && i.IsChecked).ToList();
            }
            else if (id == "sold")
            {
                if (user.Roles.Any(r => r.Role.RoleName == "Admin" || r.Role.RoleName == "Manager"))
                    items = itemsRepository.Items.Where(i => i.EndDate < DateTime.Now && i.IsSold == true).ToList();
                else
                    items = itemsRepository.Items.Where(i => i.OwnerID == user.Username &&
                                                        i.EndDate < DateTime.Now && i.IsSold == true).ToList();
            }
            else
            {
                if (user.Roles.Any(r => r.Role.RoleName == "Admin" || r.Role.RoleName == "Manager"))
                {
                    items = itemsRepository.Items.ToList();
                }
                else
                    items = itemsRepository.Items.Where(i => i.OwnerID == user.Username).ToList();
            }

            items = items.Where(i => i.Title.ToLower().Contains(key.ToLower()) || i.Description.ToLower().Contains(key.ToLower())).ToList();
            if (key != "")
                ViewData["key"] = key;
            if (category != "" && category != "All")
            {
                items = items.Where(i => i.SubCategoryID == int.Parse(category)).ToList();
                ViewData["category"] = category;
            }
            
            ViewData["roles"] = user.Roles;
            ViewData["pageSize"] = pageSize;
            ViewData["totalItems"] = items.Count();
            ViewData["currentPage"] = page;

            return View(items.Skip((page - 1) * pageSize).Take(pageSize).ToList());
        }

        // ---------------------------------------- MANAGER ----------------------------------------------------------------
        [Authorize]
        public ActionResult CheckingList()
        {
            User user = membersRepository.Members.Single(m => m.Username == HttpContext.User.Identity.Name);
            List<Item> items = new List<Item>();
            if (user.Roles.Any(r => r.Role.RoleName == "Admin" || r.Role.RoleName == "Manager"))
            {
                items = itemsRepository.Items.Where(i => i.IsChecked == false).ToList();
            }
            return View(items);
        }

        [Authorize]
        public ActionResult Check(long id, string check)
        {
            User user = membersRepository.Members.Single(m => m.Username == HttpContext.User.Identity.Name);
            if (user.Roles.Any(r => r.Role.RoleName == "Admin" || r.Role.RoleName == "Manager"))
            {
                Item item = itemsRepository.Items.Single(i => i.ItemID == id);
                item.IsChecked = (check != null) ? true : false;
                itemsRepository.Save(item);
                return Content("OK");
            }
            return Content("Thất bại");
        }

        [Authorize]
        public ActionResult Active(long id, string active)
        {
            User user = membersRepository.Members.Single(m => m.Username == HttpContext.User.Identity.Name);
            if (user.Roles.Any(r => r.Role.RoleName == "Admin" || r.Role.RoleName == "Manager"))
            {
                Item item = itemsRepository.Items.Single(i => i.ItemID == id);
                item.IsActive = (active != null) ? true : false;
                itemsRepository.Save(item);
                return Content("OK");
            }
            return Content("Thất bại");
        }

        //
        // GET: /Item/Details/5

        public ActionResult Details(int id)
        {
            var item = itemsRepository.Items.Single(i => i.ItemID == id);
            return View(item);
        }

        //
        // GET: /Item/Create

        [Authorize]
        public ActionResult Create()
        {
            //ViewData["categories"] = categoriesRepository.Categories.ToList();
            return View();
        }

        //
        // POST: /Item/Create

        [Authorize]
        [HttpPost]
        public ActionResult Create(ItemPosting model, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                Item item = new Item();
                item.IsActive = false;
                item.IsSold = false;
                item.IsChecked = false;
                item.IsAuction = model.IsAuction;
                item.Title = model.Title;
                item.Description = model.Description;
                item.OwnerID = HttpContext.User.Identity.Name;
                item.MaxPrice = model.MaxPrice;
                item.StartDate = model.StartDate;
                item.EndDate = model.EndDate;
                item.CreateDate = DateTime.Now;
                if (item.IsAuction)
                {
                    item.StartPrice = model.StartPrice;
                    //item.EndDate = DateTime.Parse(collection["EndDate"]);
                }
                else
                {
                    item.StartPrice = item.MaxPrice;
                    //item.EndDate = DateTime.MaxValue;
                }


                if (image != null)
                {
                    item.ImageMimeType = image.ContentType;
                    item.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(item.ImageData, 0, image.ContentLength);
                }
                itemsRepository.Add(item);

                return RedirectToAction("ShippingSupport", new { ItemId = item.ItemID });
            }
            else
            {
                return View(model);
            }
        }

        //----------------------------------------------------------------------------------------------------------------------------
        //  FOR SHIPPING SUPPORT ACTION AND GET ADDRESS ACTION
        //----------------------------------------------------------------------------------------------------------------------------
        [Authorize]
        public ActionResult ShippingSupport(long ItemId)
        {

            Item item = itemsRepository.Items.Single(i => i.ItemID == ItemId);
            // Editing shipping support allow for owner only
            if (HttpContext.User.Identity.Name == item.OwnerID)
            {
                ViewData["Countries"] = new DataContext(Connection.connectionString).GetTable<ShipCountry>().ToList();
                ViewData["ItemID"] = item.ItemID;
                return View(shipmentRepository.Shipments.Where(s => s.ItemID == item.ItemID).ToList());
            }
            else
            {
                TempData["error_message"] = "Bạn không được cấp quyền cho hành động này!!!";
                return View();
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult ShippingSupportAdd(FormCollection collection)
        {
            long ItemID = long.Parse(collection["ItemID"]);
            Item item = itemsRepository.Items.Single(i => i.ItemID == ItemID);

            // Add shipment for item allow for owner only
            if (HttpContext.User.Identity.Name == item.OwnerID)
            {
                Shipment shipment = new Shipment();
                shipment.AreaPartID = int.Parse(collection["PartID"]);
                shipment.ItemID = ItemID;
                shipment.Price = decimal.Parse(collection["Price"]);
                shipment.Description = collection["Description"];

                shipmentRepository.Add(shipment);
                return RedirectToAction("ShippingSupport", new { ItemId = ItemID });
            }
            else
            {
                return RedirectToAction("Index");
            }

        }

        [Authorize]
        public ActionResult ShippingSupportDelete(long id)
        {
            Shipment shipment = shipmentRepository.Shipments.Single(s => s.ShipmentID == id);
            if (shipment != null)
            {
                long ItemID = shipment.ItemID;
                shipmentRepository.Delete(shipment);
                TempData["message"] = shipment.AreaPart.CountryArea.Country.Name + " - " + shipment.AreaPart.CountryArea.AreaName +
                                        " - " + shipment.AreaPart.Name + " <b>Xóa</b> thành công..!";
                return RedirectToAction("ShippingSupport", new { ItemId = ItemID });
            }
            return RedirectToAction("Index");
        }


        public ActionResult GetCountryArea(string CountryID)
        {

            List<ShipCountryArea> areas = new DataContext(Connection.connectionString)
                                                .GetTable<ShipCountryArea>()
                                                .Where(a => a.ShipCountryID == int.Parse(CountryID)).ToList();
            string result = "<option selected=\"selected\">Chọn tỉnh/thành</option>";
            foreach (var item in areas)
            {
                result += "<option value=" + item.AreaID + " >" + item.AreaName + "</option>";
            }
            return Content(result);
        }

        public ActionResult GetAreaPart(int AreaID)
        {
            List<ShipAreaPart> parts = new DataContext(Connection.connectionString)
                                                .GetTable<ShipAreaPart>()
                                                .Where(p => p.AreaID == AreaID).ToList();
            string result = "";
            foreach (var item in parts)
            {
                result += "<option value=" + item.AreaPartID + " >" + item.Name + "</option>";
            }
            return Content(result);
        }

        // ---------------------------------------------------------------------------------------------------------------------------
        //      FOR POSTING AREA
        // ---------------------------------------------------------------------------------------------------------------------------
        [Authorize]
        public ActionResult PostingArea(long id)
        {
            Item item = itemsRepository.Items.First(i => i.ItemID == id);
            if (item.OwnerID == HttpContext.User.Identity.Name)
            {
                ViewData["showable_place"] = showablePlaces.ShowablePlaces.ToList();
                ViewData["ItemID"] = item.ItemID;
                return View(item.ItemPlaces);
            }
            else
            {
                TempData["error_message"] = "Bạn không được cấp quyền cho hành động này";
                return View();
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult PostingArea(long id, FormCollection collection)
        {
            return View();
        }

        public ActionResult GetPostingDate(FormCollection collection)
        {
            //DateTime postingDate = DateTime.Now;
            //return Content(collection["id"]);
            DateTime postingDate = itemPlaces.getShowingDate(collection["id"]);
            return Content(postingDate.ToString("dd/MM/yyyy hh:mm:ss"));
        }

        public ActionResult SavePostingPlace(FormCollection collection)
        {
            string placeName = collection["placename"];
            DateTime startDate = DateTime.Parse(collection["start"]);
            DateTime endDate = startDate.AddDays(double.Parse(collection["days"]));
            long ItemID = long.Parse(collection["itemid"]);

            ItemPlace itemPlace = new ItemPlace();
            itemPlace.PlaceName = placeName;
            itemPlace.ItemID = ItemID;
            itemPlace.StartDate = startDate;
            itemPlace.EndDate = endDate;
            itemPlace.IsPaid = false;
            itemPlace.PaidMoney = 0;

            itemPlaces.Add(itemPlace);

            return Content("Success");
        }

        public ActionResult UnSavePostingPlace(FormCollection collection)
        {
            string placeName = collection["del_placename"];
            long ItemID = long.Parse(collection["del_itemid"]);
            itemPlaces.DeleteAll(itemPlaces.ItemPlaces.Where(i => i.ItemID == ItemID && i.PlaceName == placeName).ToList());
            return Content("OK");
        }

        [Authorize]
        public ActionResult ConfirmAndPayment(long id)
        {
            Item item = itemsRepository.Items.First(i => i.ItemID == id);
            if (item.OwnerID == HttpContext.User.Identity.Name)
            {
                ViewData["showable_place"] = showablePlaces.ShowablePlaces.ToList();
                ViewData["ItemID"] = item.ItemID;
                return View(item);
            }
            else
            {
                TempData["error_message"] = "Bạn không được cấp quyền cho hành động này";
                return View();
            }
        }

        [Authorize]
        public ActionResult AcceptPayment(long id)
        {
            List<ItemPlace> _itemPlaces = itemPlaces.ItemPlaces.Where(i => i.ItemID == id).ToList();
            Item _item = itemsRepository.Items.Single(i => i.ItemID == id);
            User user = membersRepository.Members.Single(u => u.Username == HttpContext.User.Identity.Name);
            decimal totalPrice = 0;
            foreach (var item in _itemPlaces)
            {
                var _itemPlace = itemPlaces.ItemPlaces.Single(i => i.ItemPlaceID == item.ItemPlaceID);
                decimal pricePerDay = showablePlaces.ShowablePlaces.Single(s => s.PlaceName == item.PlaceName).PricePerDay;
                double showDays = (_itemPlace.EndDate - _itemPlace.StartDate).TotalDays;
                decimal price = pricePerDay * (decimal)showDays;
                totalPrice += price;
            }
            if (user.Money >= totalPrice)
            {
                decimal money = user.Money;
                decimal sumPrice = 0;
                bool addHistory = false;
                foreach (var item in _itemPlaces.Where(i => !i.IsPaid))
                {
                    addHistory = true;
                    var _itemPlace = itemPlaces.ItemPlaces.Single(i => i.ItemPlaceID == item.ItemPlaceID);

                    decimal pricePerDay = showablePlaces.ShowablePlaces.Single(s => s.PlaceName == item.PlaceName).PricePerDay;
                    double showDays = (_itemPlace.EndDate - _itemPlace.StartDate).TotalDays;
                    decimal price = pricePerDay * (decimal)showDays;
                    money -= price;
                    sumPrice += price;
                    _itemPlace.PaidMoney = price;
                    _itemPlace.IsPaid = true;
                    _item.IsActive = true;
                    itemsRepository.Save(_item);
                    itemPlaces.Save(_itemPlace);
                    user.Money = money;
                    membersRepository.Save(user);

                    HttpContext.Session["Profile"] = user;
                }
                if (addHistory)
                {
                    PaymentHistory history = new PaymentHistory();
                    history.PaidDate = DateTime.Now;
                    history.PaidMoney = sumPrice;
                    history.Username = user.Username;
                    history.TotalMoney = user.Money;
                    history.PaidContent = "Thanh toán đăng tin và vị trí đăng cho sản phẩm có mã: " + id;
                    PaymentHistoryRepository.Add(history);
                }
            }
            else
                return RedirectToAction("PaymentSuccess", new { result = "Giao dịch thất bại do tài khoản không đủ" });
            return RedirectToAction("PaymentSuccess", new { result = "Giao dịch thực hiện thành công!!!" });
        }

        public ViewResult PaymentSuccess(string result)
        {
            ViewData["message"] = result;
            return View();
        }

        //----------------------------------------------------------------------------------------------------------------------------------
        //
        // GET: /Item/Edit/5
        [Authorize]
        public ActionResult Edit(long id)
        {
            Item item = itemsRepository.Items.Single(i => i.ItemID == id);
            if (item != null && item.OwnerID == HttpContext.User.Identity.Name)
            {
                ItemPosting _item = new ItemPosting();
                _item.ItemID = item.ItemID;
                _item.Title = item.Title;
                _item.Description = item.Description;
                _item.MaxPrice = item.MaxPrice;
                _item.IsAuction = item.IsAuction;
                _item.StartDate = item.StartDate;
                _item.EndDate = item.EndDate;
                _item.StartPrice = item.StartPrice;
                return View(_item);
            }
            return RedirectToAction("Create");
        }

        //
        // POST: /Item/Edit/5
        [Authorize]
        [HttpPost]
        public ActionResult Edit(ItemPosting model, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                Item item = new Item();
                item.ItemID = model.ItemID;
                item.IsActive = false;
                item.IsSold = false;
                item.IsChecked = false;
                item.IsAuction = model.IsAuction;
                item.Title = model.Title;
                item.Description = model.Description;
                item.OwnerID = HttpContext.User.Identity.Name;
                item.MaxPrice = model.MaxPrice;
                item.StartDate = model.StartDate;
                item.EndDate = model.EndDate;
                if (item.IsAuction)
                {
                    item.StartPrice = model.StartPrice;
                    //item.EndDate = DateTime.Parse(collection["EndDate"]);
                }
                else
                {
                    item.StartPrice = item.MaxPrice;
                    //item.EndDate = DateTime.MaxValue;
                }


                if (image != null)
                {
                    item.ImageMimeType = image.ContentType;
                    item.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(item.ImageData, 0, image.ContentLength);
                }
                itemsRepository.Save(item);

                return RedirectToAction("ShippingSupport", new { ItemId = item.ItemID });
            }
            else
            {
                return View(model);
            }
        }

        //
        // GET: /Item/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Item/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //-------------- GET IMAGE FROM DATABASE ------------------------------------------------------
        public FileContentResult GetImage(long ItemID)
        {
            var item = itemsRepository.Items.First(i => i.ItemID == ItemID);
            return File(item.ImageData, item.ImageMimeType);
        }

        //
        // ITEM SESSION
        [Authorize]
        public ActionResult Session(string id,string key = "",string category="", int page = 1)
        {
            User user = membersRepository.Members.Single(m => m.Username == HttpContext.User.Identity.Name);
            List<Item> items = new List<Item>();
            int count = 0;
            if (id == "win")
            {
                var _list = itemsRepository.Items.Where(i => i.EndDate < DateTime.Now && 
                            (i.Title.ToLower().Contains(key.ToLower()) || i.Description.ToLower().Contains(key.ToLower())));
                foreach( var item in _list)
                {
                    if (item.CurUser == user.Username)
                    {
                        items.Add(item);
                        count++;
                    }
                }
                //Update showing number of win items
                HttpContext.Session["Win"] = count;
            }
            count = 0;
            if (id == "join")
            {
                foreach (var item in itemsRepository.Items.Where(i => i.Title.ToLower().Contains(key.ToLower()) || i.Description.ToLower().Contains(key.ToLower())))
                {
                    if (item.Bids.Any(b => b.BidderID == user.Username))
                    {
                        items.Add(item);
                        count++;
                    }
                }
                //Update showing number of join items
                HttpContext.Session["Join"] = count;
            }

            if (key != "")
                ViewData["key"] = key;
            if (category != "" && category != "All")
            {
                ViewData["category"] = category;
                items = items.Where(i => i.SubCategoryID == int.Parse(category)).ToList();
            }

            ViewData["pageSize"] = pageSize;
            ViewData["totalItems"] = items.Count();
            ViewData["currentPage"] = page;

            return View("index", items.Skip((page - 1) * pageSize).Take(pageSize).ToList());
        }

        [Authorize]
        public ActionResult Checking()
        {
            User user = membersRepository.Members.Single(m => m.Username == HttpContext.User.Identity.Name);
            List<Item> items = new List<Item>();
            if (user.Roles.Any(r => r.Role.RoleName == "Admin" || r.Role.RoleName == "Manager"))
            {
                items = itemsRepository.Items.Where(i => i.IsChecked == false).ToList();
            }

            ViewData["pageSize"] = pageSize;
            ViewData["totalItems"] = items.Count();
            ViewData["currentPage"] = 1;
            ViewData["roles"] = user.Roles;

            return View("List", items);
        }

        [Authorize]
        public ActionResult EditItem(long id)
        {
            User user = membersRepository.Members.Single(m => m.Username == HttpContext.User.Identity.Name);
            if (user.Roles.Any(r => r.Role.RoleName == "Admin" || r.Role.RoleName == "Manager"))
            {
                Item item = itemsRepository.Items.Single(i => i.ItemID == id);
                ViewData["roles"] = user.Roles;
                ViewData["category"] = subCategoriesRepository.subCategories.ToList();
                return View(item);
            }
            return RedirectToAction("index");
        }

        [Authorize]
        public ActionResult SetCategory(long id, int categoryID)
        {
            Item item = itemsRepository.Items.Single(i => i.ItemID == id);
            User user = membersRepository.Members.Single(m => m.Username == HttpContext.User.Identity.Name);
            if (item != null && user.Roles.Any(r => r.Role.RoleName == "Admin" || r.Role.RoleName == "Manager"))
            {
                item.SubCategoryID = categoryID;
                itemsRepository.Save(item);
                return Content("OK");
            }
            return Content("Thất bại");
        }

    }
}
