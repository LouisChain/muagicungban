using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.ComponentModel.DataAnnotations;

namespace muagicungban.Entities
{
    [Table(Name="Users")]
    public class User
    {
        [StringLength(50)]
        [Column(Name="Username", IsPrimaryKey=true)]
        public string Username { get; set; }

        [StringLength(50)]
        [Column(Name="Password")]
        public string Password { get; set; }

        [Column(Name = "RegisDate")]
        public DateTime RegisDate { get; set; }

        [Column(Name = "Birthday")]
        public DateTime Birthday { get; set; }

        [Column(Name = "Address", CanBeNull = false)]
        public string Address { get; set; }

        [StringLength(50)]
        [Column(Name="Name")]
        public string Name { get; set; }

        [StringLength(250)]
        [Column(Name="Email")]
        public string Email { get; set; }

        [StringLength(50)]
        [Column(Name="Phone")]
        public string Phone { get; set; }

        [Column(Name = "IsActive")]
        public bool IsActive { get; set; }

        [Column(Name = "IsBan")]
        public bool IsBan { get; set; }

        [Column(Name = "Money")]
        public decimal Money { get; set; }


        [System.Data.Linq.Mapping.Association(OtherKey = "UserID", ThisKey = "Username")]
        private EntitySet<UserRoles> _roles = new EntitySet<UserRoles>();
        public IList<UserRoles> Roles {
            get {
                return _roles.ToList();
            }
            //set; 
        }
/*
        [Association(OtherKey = "Username")]
        EntitySet<PaymentAccount> _paymentAccounts = new EntitySet<PaymentAccount>();
        public IList<PaymentAccount> PaymentAccounts { get { return _paymentAccounts.ToList().AsReadOnly(); } }

*/
    }
}