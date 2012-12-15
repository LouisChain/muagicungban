using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;

using muagicungban.Entities;

namespace muagicungban.Repositories
{
    public class AccountRechargeRepository
    {
        Table<AccountRecharge> accountRechargeTable;
        public AccountRechargeRepository(string connectionString)
        {
            accountRechargeTable = new DataContext(connectionString).GetTable<AccountRecharge>();
        }
        public IQueryable<AccountRecharge> AccountRecharges { get { return accountRechargeTable; } }
        public void Add(AccountRecharge item)
        {
            accountRechargeTable.InsertOnSubmit(item);
            accountRechargeTable.Context.SubmitChanges();
        }
        public void Save(AccountRecharge item)
        {
            if (accountRechargeTable.Any(a => a.RechargeID == item.RechargeID))
            {
                AccountRecharge _item = accountRechargeTable.Single(a => a.RechargeID == item.RechargeID);
                _item.CreateDate = item.CreateDate;
                _item.Username = item.Username;
                _item.Price = item.Price;
                _item.IsPaid = item.IsPaid;
                accountRechargeTable.Context.SubmitChanges();
            }
        }
    }
}