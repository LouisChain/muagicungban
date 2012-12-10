using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace muagicungban.Entities
{
    //SHIPPING ADDRESS SUPPORT INFORMATION

    [Table(Name = "Shipment")]
    public class Shipment
    {
        [Column(Name = "ShipmentID", IsDbGenerated = true, IsPrimaryKey = true, AutoSync = AutoSync.OnInsert)]
        public long ShipmentID { get; set; }

        [Column(Name = "AreaPartID")]
        public int AreaPartID { get; set; }

        [Column(Name = "ItemID")]
        public long ItemID { get; set; }

        //EntityRef<Item> _item;
        //[Association(ThisKey = "ItemID", Storage = "_item")]
        //public Item Item { 
        //    get { return _item.Entity; }
        //}

        EntityRef<ShipAreaPart> _areaPart;
        [System.Data.Linq.Mapping.Association(ThisKey = "AreaPartID", Storage = "_areaPart")]
        public ShipAreaPart AreaPart { get { return _areaPart.Entity; } }

        [Column(Name = "Price")]
        public decimal Price { get; set; }

        [StringLength(250)]
        [Column(Name = "Description", CanBeNull = true)]
        public string Description { get; set; }
    }
}