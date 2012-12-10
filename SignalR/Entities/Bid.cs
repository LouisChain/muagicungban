using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace muagicungban.Entities
{
    [Table(Name = "Bids")]
    public class Bid
    {
        [Column(Name = "BidID", IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public long BidID { get; set; }

        [Column(Name = "ItemID")]
        public long ItemID { get; set; }

        [Column(Name = "DatePlace")]
        public DateTime DatePlace { get; set; }

        [Column(Name = "Amount")]
        public decimal Amount { get; set; }

        [Column(Name = "BidderID")]
        public string BidderID { get; set; }

        
    }
}