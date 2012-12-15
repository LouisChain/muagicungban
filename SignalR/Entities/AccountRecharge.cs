using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace muagicungban.Entities
{
    [Table(Name = "AccountRecharge")]
    public class AccountRecharge
    {
        [Column(Name = "RechargeID", IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public long RechargeID { get; set; }

        [Column(Name = "CreateDate")]
        public DateTime CreateDate { get; set; }

        [Column(Name = "Username")]
        public string Username { get; set; }

        [Column(Name = "Price")]
        public decimal Price { get; set; }

        [Column(Name = "IsPaid")]
        public bool IsPaid { get; set; }
    }
}