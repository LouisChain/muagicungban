using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;

using muagicungban.Entities;

namespace muagicungban.Repositories
{
    public class ItemPlaceRepository
    {
        Table<ItemPlace> itemPlaceTable;
        ShowablePlaceRepository showablePlaceRepository;
        SubCategoriesRepository subCategoriesRepository;
        public ItemPlaceRepository(string connectionstring)
        {
            itemPlaceTable = new DataContext(connectionstring).GetTable<ItemPlace>();
            showablePlaceRepository = new ShowablePlaceRepository(connectionstring);
            subCategoriesRepository = new SubCategoriesRepository(connectionstring);
        }

        public IQueryable<ItemPlace> ItemPlaces { get { return itemPlaceTable; } }

        public void Add(ItemPlace itemPlace)
        {
            // Get the maximum number of items that can show on that place
            int numberOfItems = showablePlaceRepository.ShowablePlaces.Single(s => s.PlaceName == itemPlace.PlaceName).NumberOfItems;
            
            //Get the number of items are on showing on that place
            int itemsOnShowing = itemPlaceTable.Where(i => i.EndDate >= DateTime.Now && i.PlaceName == itemPlace.PlaceName ).ToList().Count;

            // Check, if number of items on showing are greater than or equal the maximum number of showing items
            // Then, Add it on the waiting queue
            if (itemsOnShowing >= numberOfItems)
            {
                // Get numbers of showing days
                int showingDays = itemPlace.EndDate.Subtract(itemPlace.StartDate).Days;
                
                // Get the date to show this item 
                DateTime showingDate = this.getShowingDate(itemPlace.PlaceName);
                ItemPlace _itemPlace = new ItemPlace();
                _itemPlace.PlaceName = itemPlace.PlaceName;
                _itemPlace.ItemID = itemPlace.ItemID;
                _itemPlace.StartDate = showingDate;
                _itemPlace.EndDate = showingDate.AddDays(showingDays);

                itemPlaceTable.InsertOnSubmit(_itemPlace);
            }
            // Otherwise, add it on the showing place
            else
            {
                itemPlaceTable.InsertOnSubmit(itemPlace);
            }
            itemPlaceTable.Context.SubmitChanges();
        }

        public DateTime getShowingDate(string placeName)
        {
            int numberOfItems = showablePlaceRepository.ShowablePlaces.Single(s => s.PlaceName == placeName).NumberOfItems;

            DateTime showingDate;

            if (itemPlaceTable.Any(_i => _i.PlaceName == placeName))
            {
                showingDate = itemPlaceTable.Where(i => i.PlaceName == placeName)
                                         .OrderByDescending(i => i.EndDate)
                                         .Take(numberOfItems)
                                         .OrderBy(i => i.EndDate).First().EndDate;
                showingDate.AddSeconds(1);
            }
            else
            {
                showingDate = DateTime.Now;
            }
            if (showingDate <= DateTime.Now)
                showingDate = DateTime.Now;
            return showingDate;
        }

        public void Save(ItemPlace itemPlace)
        {
            if (!itemPlaceTable.Any(i => i.ItemPlaceID == itemPlace.ItemPlaceID))
            {
                this.Add(itemPlace);
            }
            else
            {
                ItemPlace _itemPlace = itemPlaceTable.Single(i => i.ItemPlaceID == itemPlace.ItemPlaceID);
                _itemPlace.ItemID = itemPlace.ItemID;
                _itemPlace.PlaceName = itemPlace.PlaceName;
                _itemPlace.StartDate = itemPlace.StartDate;
                _itemPlace.EndDate = itemPlace.EndDate;
                _itemPlace.IsPaid = itemPlace.IsPaid;
                _itemPlace.PaidMoney = itemPlace.PaidMoney;
                itemPlaceTable.Context.SubmitChanges();
            }
        }

        public void Delete(ItemPlace itemPlace)
        {
            itemPlaceTable.DeleteOnSubmit(itemPlace);
            itemPlaceTable.Context.SubmitChanges();
        }

        public void DeleteAll(List<ItemPlace> itemPlaces)
        {
            itemPlaceTable.DeleteAllOnSubmit(itemPlaces);
            itemPlaceTable.Context.SubmitChanges();
        }
    }
}