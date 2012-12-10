using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.Linq.Mapping;

using muagicungban.Entities;
using muagicungban.Models;

namespace muagicungban.Repositories
{
    public class ShipmentRepository
    {
        Table<Shipment> shipmentTable;
        public ShipmentRepository(string connectionString)
        {
            shipmentTable = new DataContext(connectionString).GetTable<Shipment>();
        }

        public IQueryable<Shipment> Shipments { get { return shipmentTable; } }

        public void Add(Shipment shipment)
        {
            shipmentTable.InsertOnSubmit(shipment);
            shipmentTable.Context.SubmitChanges();
        }

        public void Delete(Shipment shipment)
        {
            shipmentTable.DeleteOnSubmit(shipment);
            shipmentTable.Context.SubmitChanges();
        }

        public void Save(Shipment shipment)
        {
            Shipment _temp = shipmentTable.Single(s => s.ShipmentID == shipment.ShipmentID);
            _temp.AreaPartID = shipment.AreaPartID;
            _temp.ItemID = shipment.ItemID;
            _temp.Price = shipment.Price;
            _temp.Description = shipment.Description;
            shipmentTable.Context.SubmitChanges();
        }

    }
}