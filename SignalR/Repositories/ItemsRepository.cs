using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.Linq.Mapping;

using muagicungban.Entities;

namespace muagicungban.Repositories
{
    public class ItemsRepository
    {
        private Table<Item> itemsTable;
        public ItemsRepository(string connectionString)
        {
            itemsTable = new DataContext(connectionString).GetTable<Item>();
        }

        public IQueryable<Item> Items { get { return itemsTable; } }

        public void Add(Item item)
        {
            itemsTable.InsertOnSubmit(item);
            itemsTable.Context.SubmitChanges();
        }

        public void Save(Item item)
        {
            if (!itemsTable.Any(i => i.ItemID == item.ItemID))
            {
                this.Add(item);
            }
            else
            {
                Item _item = itemsTable.Single(i => i.ItemID == item.ItemID);
                _item.Title = item.Title;
                _item.Description = item.Description;
                _item.IsAuction = item.IsAuction;
                _item.OwnerID = item.OwnerID;
                _item.StartDate = item.StartDate;
                _item.EndDate = item.EndDate;
                _item.SubCategoryID = item.SubCategoryID;
                _item.MaxPrice = item.MaxPrice;
                _item.IsActive = item.IsActive;
                _item.IsSold = item.IsSold;
                if (item.ImageData != null)
                {
                    _item.ImageData = item.ImageData;
                    _item.ImageMimeType = item.ImageMimeType;
                }
                itemsTable.Context.SubmitChanges();
            }
        }

        public void Delete(Item item)
        {
            itemsTable.DeleteOnSubmit(item);
            itemsTable.Context.SubmitChanges();
        }

        public Item FetchByID(int ID)
        {
            return itemsTable.FirstOrDefault(i => i.ItemID == ID);
        }
    }
}