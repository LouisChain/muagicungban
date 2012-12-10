using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;

using muagicungban.Entities;

namespace muagicungban.Repositories
{
    public class UserRolesRepository
    {
        private Table<UserRoles> userRolesTable;
        public UserRolesRepository(string connectionString)
        {
            userRolesTable = new DataContext(connectionString).GetTable<UserRoles>();
        }

        public IQueryable<UserRoles> UserRoles { get { return userRolesTable; } }

        public void Add(UserRoles userRoles)
        {
            userRolesTable.InsertOnSubmit(userRoles);
            userRolesTable.Context.SubmitChanges();
        }

        public void AddAll(IEnumerable<UserRoles> userRoles)
        {
            userRolesTable.InsertAllOnSubmit(userRoles);
            userRolesTable.Context.SubmitChanges();
        }

        public void Delete(UserRoles userRoles)
        {
            userRolesTable.DeleteOnSubmit(userRoles);
            userRolesTable.Context.SubmitChanges();
        }

        public void DeleteAll(IEnumerable<UserRoles> userRoles)
        {
            userRolesTable.DeleteAllOnSubmit(userRoles);
            userRolesTable.Context.SubmitChanges();
        }


    }
}