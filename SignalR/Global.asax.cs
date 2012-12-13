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
        MembersRepository membersRepository = new MembersRepository(Connection.connectionString);
        ItemPlaceRepository itemPlaceRepository = new ItemPlaceRepository(Connection.connectionString);

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
            ThreadStart DeleteUnactive = new ThreadStart(DeleteUnactiveUser);
            Thread thread = new Thread(Job);
            Thread deleteUnactive = new Thread(DeleteUnactive);
            thread.Start();
            deleteUnactive.Start();
        }

        private void CheckWinner()
        {
            while (true)
            {
                var itemsEnded = itemsRepository.Items.Where(i => i.IsChecked && i.IsActive && !i.IsSold && i.EndDate <= DateTime.Now).ToList();
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
                DeleteUnpaidItem();
                Thread.Sleep(30000);
            }
        }

        private void DeleteUnactiveUser()
        {
            Thread.Sleep(10000);
            while (true)
            {
                var users = membersRepository.Members.Where(m => !m.IsActive).ToList();
                foreach (var user in users)
                {
                    if ((DateTime.Now - user.RegisDate).TotalDays >= 3)
                    {
                        membersRepository.Delete(user);
                    }
                }
                Thread.Sleep(45000);
            }
        }

        private void DeleteUnpaidItem()
        {
                var items = itemsRepository.Items.Where(m => !m.IsActive).ToList();
                foreach (var item in items)
                {
                    if ((DateTime.Now - item.CreateDate).TotalDays >= 3)
                    {
                        itemsRepository.Delete(item);
                        itemPlaceRepository.DeleteAll(itemPlaceRepository.ItemPlaces.Where(i => i.ItemID == item.ItemID).ToList());
                    }
                }
        }
    }
}