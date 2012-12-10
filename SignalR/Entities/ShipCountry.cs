using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace muagicungban.Entities
{
    [Table(Name = "ShipCountries")]
    public class ShipCountry
    {
        [Column(Name = "ShipCountryID", IsPrimaryKey = true)]
        public int ShipCountryID { get; set; }

        [StringLength(100)]
        [Required(ErrorMessage="Country name is required!!!")]
        [Column(Name = "Name")]
        public string Name { get; set; }

        [System.Data.Linq.Mapping.Association(OtherKey = "ShipCountryID")]
        EntitySet<ShipCountryArea> _shipCountryAreas = new EntitySet<ShipCountryArea>();
        public IList<ShipCountryArea> ShipCountryAreas { get { return _shipCountryAreas.ToList().AsReadOnly(); } }

    }
}