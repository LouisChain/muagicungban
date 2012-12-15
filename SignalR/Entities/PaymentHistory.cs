using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace muagicungban.Entities
{
    [Table(Name = "PaymentsHistory")]
    public class PaymentHistory
    {
        [Column(Name = "HistoryID",IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public long HistoryID { get; set; }

        [Column(Name = "PaidDate")]
        public DateTime PaidDate { get; set; }

        [Column(Name = "Username")]
        public string Username { get; set; }

        [Column(Name = "PaidMoney")]
        public decimal PaidMoney { get; set; }

        [Column(Name = "TotalMoney")]
        public decimal TotalMoney { get; set; }

        [Column(Name = "PaidContent")]
        public string PaidContent { get; set; }
    }
}