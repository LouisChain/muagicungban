using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.Linq.Mapping;

namespace muagicungban.Models
{
    [Table(Name = "ItemImages")]
    public class ItemImage
    {
        [Column(Name = "ImageID", IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public long ImageID { get; set; }

        [Column(Name = "ItemID")]
        public int ItemID { get; set; }

        [Column(Name = "ImageName")]
        public string ImageName { get; set; }

        [Column(Name = "ImageData")]
        public byte[] ImageData { get; set; }

        [ScaffoldColumn(false)]
        [Column(Name = "MimeType")]
        public string MimeType { get; set; }

    }
}