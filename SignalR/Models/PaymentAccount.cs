using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.Linq.Mapping;

using muagicungban.Models;

namespace muagicungban.Entities
{
    [Table(Name="PaymentAccount")]
    public class PaymentAccount
    {
        [Column(Name = "PaymentID")]
        internal int PaymentID { get; set; }

        EntityRef<Payment> _payment;
        [Association(Storage = "_payment", ThisKey = "PaymentID")]
        public Payment Payment
        {
            get { return _payment.Entity; }
            internal set { _payment.Entity = value; PaymentID = value.PaymentID; }
        }

        [Column(Name = "Identity")]
        public string PaymentIdentity { get; set; }

        [Column(Name = "Username")]
        internal string Username { get; set; }
        EntityRef<User> _user;
        [Association(ThisKey = "Username", Storage = "_user")]
        public User User
        {
            get { return _user.Entity; }
            internal set { _user.Entity = value; Username = value.Username; }
        }
    }
}