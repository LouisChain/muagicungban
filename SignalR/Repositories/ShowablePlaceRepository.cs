using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;

using muagicungban.Entities;

namespace muagicungban.Repositories
{
    public class ShowablePlaceRepository
    {
        Table<ShowablePlace> showablePlaceTable;
        public ShowablePlaceRepository(string connectionString)
        {
            showablePlaceTable = new DataContext(connectionString).GetTable<ShowablePlace>();
        }

        public IQueryable<ShowablePlace> ShowablePlaces { get { return showablePlaceTable; } }
    }
}