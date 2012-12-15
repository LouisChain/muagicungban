using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;

using muagicungban.Entities;

namespace muagicungban.Repositories
{
    public class PaymentHistoryRepository
    {
        Table<PaymentHistory> PaymentTable;
        public PaymentHistoryRepository(string connectionString)
        {
            PaymentTable = new DataContext(connectionString).GetTable<PaymentHistory>();
        }
        public IQueryable<PaymentHistory> PaymentHistorys { get { return PaymentTable; } }
        public void Add(PaymentHistory item)
        {
            PaymentTable.InsertOnSubmit(item);
            PaymentTable.Context.SubmitChanges();
        }
    }
}