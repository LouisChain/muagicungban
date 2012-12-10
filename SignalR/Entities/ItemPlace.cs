using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace muagicungban.Entities
{
    [Table(Name="ItemPlace")]
    public class ItemPlace
    {

        [Column(Name = "ItemPlaceID", IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public long ItemPlaceID { get; set; }

        [Column(Name="PlaceName")] // Foreign key to [ShowablePlace]
        public string PlaceName { get; set; }

        [Column(Name="ItemID")]
        public long ItemID { get; set; }

        [Column(Name = "StartDate")]
        public DateTime StartDate { get; set; }

        [Column(Name = "EndDate")]
        public DateTime EndDate { get; set; }

        [Column(Name = "IsPaid")]
        public bool IsPaid { get; set; }

        [Column(Name = "PaidMoney", CanBeNull = true)]
        public decimal PaidMoney { get; set; }

        EntityRef<ShowablePlace> _place;
        [Association(ThisKey = "PlaceName", Storage = "_place")]
        public ShowablePlace Place { get { return _place.Entity; } }
    }
}