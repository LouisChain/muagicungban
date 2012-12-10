using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace muagicungban.Entities
{
    [Table(Name="ShowablePlace")]
    public class ShowablePlace
    {
        [Column(Name = "PlaceName", IsPrimaryKey = true)]
        public string PlaceName { get; set; }

        [Column(Name = "PlaceDescription", CanBeNull = true)]
        public string PlaceDescription { get; set; }

        [Column(Name = "PricePerDay")]
        public decimal PricePerDay { get; set; }

        [Column(Name = "NumberOfItems")]
        public int NumberOfItems { get; set; }

    }
}