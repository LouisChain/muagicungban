using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.ComponentModel.DataAnnotations;

namespace muagicungban.Entities
{
    [Table(Name = "UserRoles")]
    public class UserRoles
    {
        [Column(Name = "ID", IsPrimaryKey = true, IsDbGenerated=true, AutoSync=AutoSync.OnInsert)]
        private long ID { get; set; }

        [Column(Name = "RoleID")]
        public int RoleID { get; set; }

        [StringLength(50)]
        [Column(Name = "UserID")]
        public string UserID { get; set; }

        
        EntityRef<Role> _role;
        [System.Data.Linq.Mapping.Association(ThisKey = "RoleID", OtherKey= "RoleID", Storage = "_role")]
        public Role Role { get { return _role.Entity; } }
        
    }
}