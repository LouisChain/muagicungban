using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace muagicungban.Entities
{
    [Table(Name = "ShipCountryArea")]
    public class ShipCountryArea
    {
        [Column(Name = "ShipCountryID")]
        public int ShipCountryID { get; set; }

        [Column(Name = "AreaID", IsPrimaryKey = true)]
        public int AreaID { get; set; }

        
        [Column(Name = "AreaName")]
        public string AreaName { get; set; }

        EntityRef<ShipCountry> _country;
        [Association(ThisKey = "ShipCountryID", Storage = "_country")]
        public ShipCountry Country { get { return _country.Entity; } }
    }
}