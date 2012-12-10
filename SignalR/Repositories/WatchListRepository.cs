using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;

using muagicungban.Entities;

namespace muagicungban.Repositories
{
    public class WatchListRepository
    {
        Table<WatchList> watchListTable;
        public WatchListRepository(string connectionString)
        {
            watchListTable = new DataContext(connectionString).GetTable<WatchList>();
        }

        public IQueryable<WatchList> WatchLists { get { return watchListTable; } }

        public void Add(WatchList watchList)
        {
            watchListTable.InsertOnSubmit(watchList);
            watchListTable.Context.SubmitChanges();
        }

        public void Delete(WatchList watchList)
        {
            watchListTable.DeleteOnSubmit(watchList);
            watchListTable.Context.SubmitChanges();
        }
    }
}