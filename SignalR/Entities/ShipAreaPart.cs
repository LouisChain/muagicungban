using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace muagicungban.Entities
{
    [Table(Name = "ShipAreaPart")]
    public class ShipAreaPart
    {
        [Column(Name = "AreaID")]
        public int AreaID { get; set; }

        [Column(Name = "AreaPartID", IsPrimaryKey = true)]
        public int AreaPartID { get; set; }


        [Column(Name = "PartName")]
        public string Name { get; set; }

        [Association(OtherKey = "AreaPartID")]
        EntitySet<Shipment> _shipments = new EntitySet<Shipment>();
        public IList<Shipment> Shipments { get { return _shipments.ToList().AsReadOnly(); } }

        EntityRef<ShipCountryArea> _countryArea;
        [Association(ThisKey = "AreaID", Storage = "_countryArea")]
        public ShipCountryArea CountryArea { get { return _countryArea.Entity; } }

    }
}