using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.Linq.Mapping;



namespace muagicungban.Entities
{
    [Table(Name = "Payments")]
    public class Payment
    {
        [Column(Name = "PaymentID", IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        internal int PaymentID { get; set; }

        [Column(Name = "Method")]
        public string Method { get; set; }

        [Column(Name = "Provider")]
        public string Provider { get; set; }

        [Column(Name = "Description")]
        public string Description { get; set; }

        [Association(OtherKey="PaymentID")]
        EntitySet<PaymentAccount> _paymentAccounts = new EntitySet<PaymentAccount>();
        public IList<PaymentAccount> PaymentAccounts
        {
            get { return _paymentAccounts.ToList().AsReadOnly(); }
        }
    }
}