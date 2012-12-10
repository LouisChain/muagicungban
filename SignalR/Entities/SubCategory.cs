using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq.Mapping;
using System.Data.Linq;
using System.ComponentModel.DataAnnotations;

namespace muagicungban.Models
{
    [Table(Name="SubCategories")]
    public class SubCategory
    {
        [System.Web.Mvc.HiddenInput(DisplayValue=false)]
        [Column(Name="SubCategoryID", IsPrimaryKey=true, IsDbGenerated=true, AutoSync=AutoSync.OnInsert)]
        public int ID { get; set; }

        [StringLength(100)]
        [Column(Name="SubName")]
        public string Name { get; set; }

        [Column(Name="CategoryID")]
        public int CategoryID { get; set; }

        //[Association(OtherKey = "SubCategoryID", ThisKey="SubCategoryID")]
        //private EntitySet<Item> _items = new EntitySet<Item>();
        //public IList<Item> Items
        //{
        //    get { return _items.ToList().AsReadOnly(); }
        //}

        EntityRef<Category> _category;
        [System.Data.Linq.Mapping.Association(ThisKey = "CategoryID", Storage = "_category")]
        public Category Category { get { return _category.Entity; } }
    }
}