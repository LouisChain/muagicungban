using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

using muagicungban.Abstract;
using muagicungban.Models;
using muagicungban.Repositories;

namespace muagicungban
{
    public class MyRoleProvider : RoleProvider
    {
        private IMemberRepository membersRepository;
        public MyRoleProvider()
        {
            membersRepository = new MembersRepository(Connection.connectionString);
        }

        public override string[] GetRolesForUser(string username)
        {
            //var user = membersRepository.Members.First(m => m.Username == username);
            //string[] roles = new string[user.Roles.Count];
            //int i = 0;
            //foreach (var role in user.Roles)
            //{
            //    roles[i++] = role.Role.Name;
            //}
            //return roles;
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void Initialize(string name, System.Collections.Specialized.NameValueCollection config)
        {
            base.Initialize(name, config);
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return base.ToString();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override string Description
        {
            get
            {
                return base.Description;
            }
        }

        public override string Name
        {
            get
            {
                return base.Name;
            }
        }
    }
}