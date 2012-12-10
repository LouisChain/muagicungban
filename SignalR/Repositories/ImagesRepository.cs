using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;

using muagicungban.Models;

namespace muagicungban.Repositories
{
    public class ImagesRepository
    {
        private Table<ItemImage> imagesTable;
        public ImagesRepository(string connectionString)
        {
            imagesTable = new DataContext(connectionString).GetTable<ItemImage>();
        }

        public IQueryable<ItemImage> itemImages { get { return imagesTable; } }

        public void Add(ItemImage _itemImage)
        {
            imagesTable.InsertOnSubmit(_itemImage);
            imagesTable.Context.SubmitChanges();
        }

        public void Save(ItemImage _itemImage)
        {
            if (!imagesTable.Any(i => i.ImageID == _itemImage.ImageID))
            {
                this.Add(_itemImage);
            }
            else
            {
                ItemImage _image = imagesTable.Single(i => i.ImageID == _itemImage.ImageID);
                _image.ImageName = _itemImage.ImageName;
                _image.MimeType = _itemImage.MimeType;
                _image.ImageData = _itemImage.ImageData;
                _image.ItemID = _itemImage.ItemID;
                imagesTable.Context.SubmitChanges();
            }
        }

        public void Delete(ItemImage _itemImage)
        {
            imagesTable.DeleteOnSubmit(_itemImage);
            imagesTable.Context.SubmitChanges();
        }
    }
}