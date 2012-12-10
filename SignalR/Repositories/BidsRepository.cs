using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;

using muagicungban.Entities;

namespace muagicungban.Repositories
{
    public class BidsRepository
    {
        Table<Bid> bidsTable;
        public BidsRepository(string connectionString)
        {
            bidsTable = new DataContext(connectionString).GetTable<Bid>();
        }

        public IQueryable<Bid> Bids { get { return bidsTable; } }

        public void Add(Bid bid)
        {
            bidsTable.InsertOnSubmit(bid);
            bidsTable.Context.SubmitChanges();
        }

        public void Delete(Bid bid)
        {
            bidsTable.DeleteOnSubmit(bid);
            bidsTable.Context.SubmitChanges();
        }


    }
}