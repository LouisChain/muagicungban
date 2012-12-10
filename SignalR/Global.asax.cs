using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Threading;

using muagicungban.Entities;
using muagicungban.Repositories;

namespace muagicungban
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        ItemsRepository itemsRepository = new ItemsRepository(Connection.connectionString);
        OrdersRepository ordersRepository = new OrdersRepository(Connection.connectionString);

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Item", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterRoutes(RouteTable.Routes);

            ThreadStart Job = new ThreadStart(CheckWinner);
            Thread thread = new Thread(Job);
            thread.Start();
        }

        private void CheckWinner()
        {
            while (true)
            {
                var itemsEnded = itemsRepository.Items.Where(i => i.IsChecked && i.IsActive && i.IsAuction && !i.IsSold && i.EndDate <= DateTime.Now).ToList();
                foreach (var item in itemsEnded)
                {
                    Order order = new Order();
                    order.BuyerID = item.CurUser;
                    order.ItemID = item.ItemID;
                    order.OrderDate = DateTime.Now;
                    order.DeliveryDate = DateTime.MaxValue;
                    order.IsDelivery = false;
                    order.IsPaid = false;
                    order.AllowEditShipment = true;
                    order.ItemPrice = item.CurPrice;
                    order.ShipPrice = 0;
                    ordersRepository.Add(order);
                    Item _item = item;
                    _item.IsSold = true;
                    itemsRepository.Save(_item);
                }
                Thread.Sleep(30000);
            }
        }
    }
}