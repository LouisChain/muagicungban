using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace muagicungban.Entities
{
    [Table(Name="WatchList")]
    public class WatchList
    {
        [Column(Name = "ID", IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public long ID { get; set; }

        [Column(Name = "ItemID")]
        public long ItemID { get; set; }

        [Column(Name = "Username")]
        public string Username { get; set; }
    }
}