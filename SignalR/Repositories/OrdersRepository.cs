using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.Linq.Mapping;

using muagicungban.Entities;

namespace muagicungban.Repositories
{
    public class OrdersRepository
    {
        Table<Order> ordersTable;
        public OrdersRepository(string connectionString)
        {
            ordersTable = new DataContext(connectionString).GetTable<Order>();
        }

        public IQueryable<Order> Orders { get { return ordersTable; } }

        public void Add(Order order)
        {

            ordersTable.InsertOnSubmit(order);
            ordersTable.Context.SubmitChanges();
        }

        public void Save(Order order)
        {
            if (ordersTable.Any(o => o.OrderID == order.OrderID))
            {
                Order _order = ordersTable.Single(o => o.OrderID == order.OrderID);
                _order = order;
                ordersTable.Context.SubmitChanges();
            }
            else
            {
                this.Add(order);
            }
        }

        public void Delete(Order order)
        {
            ordersTable.DeleteOnSubmit(order);
            ordersTable.Context.SubmitChanges();
        }
    }
}