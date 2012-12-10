using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace muagicungban.Models
{
    // SHIPPING ADDRESS FOR SOLD ITEM AND OTHER CUSTOMER INFORMATION

    [Table(Name="Shipping")]
    public class Shipping
    {
        [Column(Name = "ItemID", IsPrimaryKey = true)]
        internal int ItemID { get; set; }

        [Column(Name = "AreaPartID")]
        internal int AreaPartID { get; set; }

        [Column(Name = "Street")]
        public string Street { get; set; }

        [Column(Name = "HouseNumber")]
        public string HouseNumber { get; set; }

        [Column(Name = "ShipFor")]
        public string ShipFor { get; set; }

        [Column(Name = "ShipDate")]
        public DateTime ShipDate { get; set; }
    }
}