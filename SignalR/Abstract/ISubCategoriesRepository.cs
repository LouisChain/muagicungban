using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using muagicungban.Models;

namespace muagicungban.Abstract
{
    public interface ISubCategoriesRepository
    {
        IQueryable<SubCategory> subCategories { get;}
        SubCategory FetchByID(int ID);
        void Add(SubCategory subCategory);
        void Save(SubCategory subCategory);
        void Delete(SubCategory subCategory);
    }
}
