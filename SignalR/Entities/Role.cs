using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Web;

namespace muagicungban.Entities
{
    [Table(Name="Roles")]
    public class Role
    {
        [Column(Name="RoleID", IsPrimaryKey=true, IsDbGenerated=true,AutoSync=AutoSync.OnInsert)]
        public int RoleID { get; set; }

        [Column(Name="RoleName")]
        public string RoleName { get; set; }

        [Column(Name = "Description")]
        public string Description { get; set; }
    }
}