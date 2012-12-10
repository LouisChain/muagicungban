using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.Linq.Mapping;
using System.Web.Security;
using System.ComponentModel.DataAnnotations;


namespace muagicungban.Entities
{
    [Table(Name = "Items")]
    //[InheritanceMapping(Code="A", Type=typeof(AuctionItem))]
    //[InheritanceMapping(Code="S", Type=typeof(SellItem))]
    //[InheritanceMapping(Code="U", Type=typeof(UnsetItem), IsDefault=true)]
    public class Item
    {
        [Column(Name = "ItemID", IsPrimaryKey = true, IsDbGenerated = true, AutoSync = AutoSync.OnInsert)]
        public long ItemID { get; internal set; }

        [Column(Name = "IsAuction")]
        public bool IsAuction { get; set; }

        [StringLength(250)]
        [Column(Name = "Title")]
        public string Title { get; set; }

        [System.ComponentModel.DataAnnotations.DataType(System.ComponentModel.DataAnnotations.DataType.MultilineText)]
        [Column(Name = "Description", CanBeNull = true)]
        public string Description { get; set; }

        [Column(Name = "OwnerID")]
        public string OwnerID { get; set; }

        EntityRef<User> _owner;
        [System.Data.Linq.Mapping.Association(ThisKey = "OwnerID", OtherKey="Username", Storage = "_owner")]
        public User Owner { get { return _owner.Entity; } }

        [Column(Name = "SubCategoryID", CanBeNull = true)]
        public int SubCategoryID { get; set; }

        [Column(Name = "BuyerID", CanBeNull = true)]
        public string BuyerID { get; set; }

        /*
                internal EntityRef<SubCategory> _subCategory;
                [Association(ThisKey = "SubID", Storage = "_subCategory", IsForeignKey = true)]
                public SubCategory SubCategory
                {
                    get { return _subCategory.Entity; }
                }
        */

        [Column(Name = "IsActive")]
        public bool IsActive { get; set; }

        [Column(Name = "IsSold")]
        public bool IsSold { get; set; }

        [Column(Name = "MaxPrice", CanBeNull = true)]
        public decimal MaxPrice { get; set; }

        [DataType(DataType.DateTime)]
        [Column(Name = "StartDate", CanBeNull = true)]
        public DateTime StartDate { get; set; }

        [DataType(DataType.DateTime)]
        [Column(Name = "EndDate", CanBeNull = true)]
        public DateTime EndDate { get; set; }

        [Column(Name = "StartPrice", CanBeNull = true)]
        public decimal StartPrice { get; set; }

        [Column(Name = "ImageData", CanBeNull = true)]
        public byte[] ImageData { get; set; }

        [ScaffoldColumn(false)]
        [Column(Name = "ImageMimeType", CanBeNull = true)]
        public string ImageMimeType { get; set; }

        [Column(Name = "IsChecked", CanBeNull = true)]
        public bool IsChecked { get; set; }

        [Column(Name = "CreateDate")]
        public DateTime CreateDate { get; set; }

        [System.Data.Linq.Mapping.Association(OtherKey = "ItemID")]
        EntitySet<Bid> _bids = new EntitySet<Bid>();
        public IList<Bid> Bids { get { return _bids.ToList(); } }

        [System.Data.Linq.Mapping.Association(OtherKey = "ItemID")]
        EntitySet<Shipment> _shipments = new EntitySet<Shipment>();
        public IList<Shipment> Shipments { get { return _shipments.ToList(); } }

        [System.Data.Linq.Mapping.Association(OtherKey = "ItemID")]
        EntitySet<ItemPlace> _itemPlaces = new EntitySet<ItemPlace>();
        public IList<ItemPlace> ItemPlaces { get { return _itemPlaces.ToList(); } }

        public decimal CurPrice
        {
            get
            {
                if (!_bids.Any()) return this.StartPrice;
                return _bids.OrderByDescending(b => b.Amount).First().Amount;
            }
        }

        public string CurUser
        {
            get
            {
                if (!_bids.Any()) return ".......";
                return _bids.OrderByDescending(b => b.Amount).First().BidderID;
            }
        }
        /*
                [Association(OtherKey = "ItemID")]
                EntitySet<ItemImage> _images = new EntitySet<ItemImage>();
                public IList<ItemImage> Images { get { return _images.ToList(); } }
        */


        //    public class AuctionItem : Item
        //    {
        //        [Column(Name = "StartPrice")]
        //        public decimal StartPrice { get; set; }

        //        [Column(Name = "PriceStep")]
        //        public decimal PriceStep { get; set; }

        ///*
        //        [Association(OtherKey = "ItemID")]
        //        EntitySet<Bid> _bids = new EntitySet<Bid>();
        //        public IList<Bid> Bids { get { return _bids.ToList().AsReadOnly(); } }
        //*/

        //    }

        //    public class SellItem : Item
        //    {

        //    }

        //    public class UnsetItem : Item
        //    {
        //        [Column(Name = "StartPrice")]
        //        public decimal StartPrice { get; set; }
        //    }
    }
}