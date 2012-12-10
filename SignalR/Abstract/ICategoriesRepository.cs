using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using muagicungban.Models;

namespace muagicungban.Abstract
{
    public interface ICategoriesRepository
    {
        IQueryable<Category> Categories { get; }
        Category FetchByID(int ID);
        void Add(Category category);
        void Save(Category category);
        void Delete(Category category);
    }
}
