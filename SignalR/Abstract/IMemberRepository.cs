using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using muagicungban.Entities;

namespace muagicungban.Abstract
{
    public interface IMemberRepository
    {
        void Add(User member);
        IQueryable<User> Members { get; }
        User FetchByLoginName(string loginName);
        void Save(User member);
        void Delete(User member);
        void SubmitChanges();
    }
}