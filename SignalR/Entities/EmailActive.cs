using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace muagicungban.Entities
{
    [Table(Name = "EmailActive")]
    public class EmailActive
    {
        [Column(Name = "Username", IsPrimaryKey = true)]
        public string Username { get; set; }

        [Column(Name = "Code")]
        public string Code { get; set; }
    }
}