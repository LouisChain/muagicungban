using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;

using muagicungban.Entities;

namespace muagicungban.Repositories
{
    public class EmailActiveRepository
    {
        Table<EmailActive> emailActiveTable;
        public EmailActiveRepository(string connectionString)
        {
            emailActiveTable = new DataContext(connectionString).GetTable<EmailActive>();
        }
        public IQueryable<EmailActive> EmailActives { get { return emailActiveTable; } }
        public void Add(EmailActive email)
        {
            emailActiveTable.InsertOnSubmit(email);
            emailActiveTable.Context.SubmitChanges();
        }
        public void Delete(EmailActive email)
        {
            emailActiveTable.DeleteOnSubmit(email);
            emailActiveTable.Context.SubmitChanges();
        }
    }
}