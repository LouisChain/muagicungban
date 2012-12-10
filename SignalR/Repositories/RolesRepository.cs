using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;

using muagicungban.Entities;
using muagicungban.Models;

namespace muagicungban.Repositories
{
    public class RolesRepository
    {
        private Table<Role> rolesTable;
        public RolesRepository(string connectionString)
        {
            rolesTable = new DataContext(connectionString).GetTable<Role>();
        }

        public IQueryable<Role> Roles { get { return rolesTable; } }

        public void Add(Role role)
        {
            rolesTable.InsertOnSubmit(role);
            rolesTable.Context.SubmitChanges();
        }

        public void Save(Role role)
        {
            if (rolesTable.Any(r => r.RoleID == role.RoleID))
            {
                Role _role = rolesTable.Single(r => r.RoleID == role.RoleID);
                _role.RoleName = role.RoleName;
                rolesTable.Context.SubmitChanges();
            }
        }

        public void Delete(Role role)
        {
            rolesTable.DeleteOnSubmit(role);
            rolesTable.Context.SubmitChanges();
        }
    }
}