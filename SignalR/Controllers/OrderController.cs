using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Linq;

using muagicungban.Entities;
using muagicungban.Repositories;

namespace muagicungban.Controllers
{
    public class OrderController : Controller
    {
        OrdersRepository ordersRepository = new OrdersRepository(Connection.connectionString);
        MembersRepository membersRepository = new MembersRepository(Connection.connectionString);
        ShipmentRepository shipmentRepository = new ShipmentRepository(Connection.connectionString);
        //
        // GET: /Order/
        [Authorize]
        public ActionResult Index(string type, string status) // type = buy || type = sell
        {
            List<Order> orders = new List<Order>();
            User user = membersRepository.Members.Single(u => u.Username == HttpContext.User.Identity.Name);
            if (type == "all")
                foreach (var order in ordersRepository.Orders.Where(o => o.Item.OwnerID == user.Username || o.BuyerID == user.Username))
                    orders.Add(order);
            if (type == "buy")
                foreach (var order in ordersRepository.Orders.Where(o => o.BuyerID == user.Username))
                    orders.Add(order);
            if (type == "sell")
                foreach (var order in ordersRepository.Orders.Where(o => o.Item.OwnerID == user.Username))
                    orders.Add(order);

            int notPaid = 0, paidNotDelivery = 0, paidDelivery = 0, buy = 0, sell = 0;
            notPaid = orders.Where(o => o.IsPaid == false).Count();
            paidNotDelivery = orders.Where(o => o.IsPaid == true && o.IsDelivery == false).Count();
            paidDelivery = orders.Where(o => o.IsPaid && o.IsDelivery).Count();
            buy = ordersRepository.Orders.Where(o => o.BuyerID == user.Username).Count();
            sell = ordersRepository.Orders.Where(o => o.Item.OwnerID == user.Username).Count();

            ViewData["not_paid"] = notPaid;
            ViewData["paid_not_delivery"] = paidNotDelivery;
            ViewData["paid_delivery"] = paidDelivery;
            ViewData["buy"] = buy;
            ViewData["sell"] = sell;
            
            ViewData["type"] = type;
            ViewData["status"] = status;
            if (status == "1")
                orders = orders.Where(o => o.IsPaid == false).ToList();
            if (status == "2")
                orders = orders.Where(o => o.IsPaid && o.IsDelivery == false).ToList();
            if (status == "3")
                orders = orders.Where(o => o.IsDelivery && o.IsPaid).ToList();
            return View(orders);
        }

        [Authorize]
        public ActionResult Detail(long id)
        {
            User user = membersRepository.Members.Single(u => u.Username == HttpContext.User.Identity.Name);
            Order order = ordersRepository.Orders.Single(o => o.OrderID == id);

            if (order.Item.OwnerID == user.Username || order.BuyerID == user.Username)
            {
                List<ShipCountry> countries = new List<ShipCountry>();
                List<Shipment> shipments = shipmentRepository.Shipments.Where(s => s.ItemID == id).ToList();
                foreach (var item in shipments)
                {
                    if (!countries.Any(c => c.ShipCountryID == item.AreaPart.CountryArea.Country.ShipCountryID))
                        countries.Add(item.AreaPart.CountryArea.Country);
                }
                ViewData["countries"] = countries;
                ViewData["type"] = (user.Username == order.Item.OwnerID) ? "sell" : "buy";
                return View(order);
            }
            else
                return Redirect(Url.Action("Index", "Item"));
        }


        public ActionResult GetCountryArea(long id, string CountryID)
        {
            List<Shipment> shipments = shipmentRepository.Shipments
                                            .Where(s => s.ItemID == id)
                                            .ToList();
            List<ShipCountryArea> areas = new List<ShipCountryArea>();
            foreach (var shipment in shipments)
            {
                if (shipment.AreaPart.CountryArea.Country.ShipCountryID == int.Parse(CountryID) 
                    && !areas.Any(a => a.AreaID == shipment.AreaPart.CountryArea.AreaID))
                    areas.Add(shipment.AreaPart.CountryArea);
            }
            string result = "<option selected=\"selected\">Tỉnh thành</option>";
            foreach (var item in areas)
            {
                result += "<option value=" + item.AreaID + " >" + item.AreaName + "</option>";
            }
            return Content(result);
        }

        public ActionResult GetAreaPart(int id, int AreaID)
        {
            List<Shipment> shipments = shipmentRepository.Shipments
                                            .Where(s => s.ItemID == id)
                                            .ToList();
            List<ShipAreaPart> parts = new List<ShipAreaPart>();
            foreach (var ship in shipments)
            {
                if (ship.AreaPart.AreaID == AreaID && !parts.Any(p => p.AreaPartID == ship.AreaPartID))
                    parts.Add(ship.AreaPart);
            }
            string result = "";
            foreach (var item in parts)
            {
                result += "<option value=" + item.AreaPartID + " >" + item.Name + "</option>";
            }
            return Content(result);
        }

        [Authorize]
        public ActionResult CheckDelivery(long id, bool IsDelivery)
        {
            Order order = ordersRepository.Orders.Single(o => o.OrderID == id);
            if (order.Item.OwnerID == HttpContext.User.Identity.Name)
            {
                order.IsDelivery = IsDelivery;
                ordersRepository.Save(order);
            }
            return Content("");
        }

        [Authorize]
        public ActionResult CheckPaid(long id, bool IsPaid)
        {
            Order order = ordersRepository.Orders.Single(o => o.OrderID == id);
            if (order.Item.OwnerID == HttpContext.User.Identity.Name)
            {
                order.IsPaid = IsPaid;
                ordersRepository.Save(order);
            }
            return Content("");
        }

        [Authorize]
        public ActionResult CheckAllowEditShipment(long id, bool AllowEditShipment)
        {
            Order order = ordersRepository.Orders.Single(o => o.OrderID == id);
            if (order.Item.OwnerID == HttpContext.User.Identity.Name)
            {
                order.AllowEditShipment = AllowEditShipment;
                ordersRepository.Save(order);
            }
            return Content("");
        }

        [Authorize]
        public ActionResult UpdatePaymentMethod(long id, string PaymentMethod)
        {
            Order order = ordersRepository.Orders.Single(o => o.OrderID == id);
            if (order.Item.OwnerID == HttpContext.User.Identity.Name)
            {
                order.PaymentMethod = PaymentMethod;
                ordersRepository.Save(order);
            }
            return Content("");
        }

        [Authorize]
        public ActionResult UpdateShipmentInfo(long id, FormCollection collection)
        {
            Order order = ordersRepository.Orders.Single(o => o.OrderID == id);
            List<Shipment> shipments = shipmentRepository.Shipments.Where(s => s.ItemID == order.ItemID).ToList();

            if (order.BuyerID == HttpContext.User.Identity.Name)
            {
                if (collection["PartID"] != null)
                {
                    order.DeliveryPartID = int.Parse(collection["PartID"]);
                    Shipment ship = shipments.Single(s => s.AreaPartID == order.DeliveryPartID);
                    order.ShipPrice = ship.Price;
                }
                if (collection["Address"] != null)
                    order.DeliveryAddress = collection["Address"];
                if (collection["Name"] != null)
                    order.ReceiverName = collection["Name"];
                if (collection["Phone"] != null)
                    order.ReceiverPhone = collection["Phone"];
                if (collection["Email"] != null)
                    order.ReceiverEmail = collection["Email"];
                ordersRepository.Save(order);
            }
            return Content(order.ShipPrice.ToString());
        }

        public ActionResult GetPartPrice(long id, int pID)
        {
            Shipment ship = shipmentRepository.Shipments.Single(s => s.ItemID == id && s.AreaPartID == pID);
            return Content(ship.Price.ToString());
        }
    }
}
