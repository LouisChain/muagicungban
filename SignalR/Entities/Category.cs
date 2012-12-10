using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq.Mapping;
using System.Web.Mvc;

namespace muagicungban.Models
{
    [Table(Name="Categories")]
    public class Category
    {
        [HiddenInput(DisplayValue = false)]
        [Column(Name = "CategoryID", IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public int CategoryID { get; set; }

        [StringLength(250)]
        [Required]
        [Column(Name = "CategoryName")]
        public string CategoryName { get; set; }

        [System.Data.Linq.Mapping.Association(OtherKey = "CategoryID", IsForeignKey=true)]
        private EntitySet<SubCategory> _subCategories = new EntitySet<SubCategory>();
        public IList<SubCategory> SubCategories { get { return _subCategories.ToList().AsReadOnly(); } }

    }
}