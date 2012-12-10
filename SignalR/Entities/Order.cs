using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace muagicungban.Entities
{
    [Table(Name="Orders")]
    public class Order
    {
        public Order()
        {
            this.IsPaid = false;
            this.IsDelivery = false;
        }

        [Column(Name = "OrderID", IsDbGenerated = true, IsPrimaryKey = true, AutoSync = AutoSync.OnInsert)]
        public long OrderID { get; set; }

        [Column(Name = "ItemID")]
        public long ItemID { get; set; }

        [Column(Name = "BuyerID")]
        public string BuyerID { get; set; }

        [Column(Name = "OrderDate")]
        public DateTime OrderDate { get; set; }

        [Column(Name = "ItemPrice")]
        public decimal ItemPrice { get; set; }

        [Column(Name = "ShipPrice", CanBeNull = true)]
        public decimal ShipPrice { get; set; }

        [Column(Name = "ReceiverName", CanBeNull = true)]
        public string ReceiverName { get; set; }

        [Column(Name = "ReceiverPhone", CanBeNull = true)]
        public string ReceiverPhone { get; set; }

        [Column(Name = "ReceiverEmail", CanBeNull = true)]
        public string ReceiverEmail { get; set; }

        [Column(Name = "DeliveryPartID", CanBeNull = true)]
        public int DeliveryPartID { get; set; }

        [Column(Name = "DeliveryAddress", CanBeNull = true)]
        public string DeliveryAddress { get; set; }

        [Column(Name = "DeliveryDate", CanBeNull = true)]
        public DateTime DeliveryDate { get; set; }

        [Column(Name = "IsDelivery")]
        public bool IsDelivery { get; set; }

        [Column(Name = "IsPaid")]
        public bool IsPaid { get; set; }

        [Column(Name = "AllowEditShipment")]
        public bool AllowEditShipment { get; set; }

        [Column(Name = "PaymentMethod")]
        public string PaymentMethod { get; set; }

        EntityRef<Item> _item;
        [Association(ThisKey = "ItemID", OtherKey = "ItemID", Storage = "_item")]
        public Item Item { get { return _item.Entity; } }

        EntityRef<User> _user;
        [Association(ThisKey = "BuyerID", OtherKey = "Username", Storage = "_user")]
        public User Buyer { get { return _user.Entity; } }

        EntityRef<ShipAreaPart> _shippart;
        [Association(ThisKey = "DeliveryPartID", OtherKey = "AreaPartID", Storage = "_shippart")]
        public ShipAreaPart DeliveryPart { get { return _shippart.Entity; } }
    }
}