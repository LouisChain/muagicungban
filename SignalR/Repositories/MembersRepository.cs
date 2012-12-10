using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;

using muagicungban.Entities;
using muagicungban.Abstract;

namespace muagicungban.Repositories
{
    public class MembersRepository : IMemberRepository
    {
        private Table<User> membersTable;

        public MembersRepository(string connectionString)
        {
            DataContext dc = new DataContext(connectionString);
            membersTable = dc.GetTable<User>();
        }

        public IQueryable<User> Members { get { return membersTable; } }

        public void Add(User member)
        {
            membersTable.InsertOnSubmit(member);
            membersTable.Context.SubmitChanges();
        }

        public void Save(User member)
        {
            if (!membersTable.Any(m => m.Username == member.Username))
            {
                this.Add(member);
            }
            else
            {
                User _member = membersTable.Single(m => m.Username == member.Username);
                _member.Name = member.Name;
                _member.Email = member.Email;
                _member.Phone = member.Phone;
                _member.Password = member.Password;
                membersTable.Context.SubmitChanges();
            }
            
        }

        public void Delete(User member)
        {
            membersTable.DeleteOnSubmit(member);
            membersTable.Context.SubmitChanges();
        }

        public void SubmitChanges()
        {
            membersTable.Context.SubmitChanges();
        }

        public User FetchByLoginName(string loginName)
        {
            return membersTable.FirstOrDefault(m => m.Username == loginName);
        }
    }
}