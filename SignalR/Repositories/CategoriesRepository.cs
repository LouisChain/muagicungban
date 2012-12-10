using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;
using System.Data.Linq.Mapping;

using muagicungban.Models;
using muagicungban.Abstract;

namespace muagicungban.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private Table<Category> categoriesTable;
        public CategoriesRepository(string connectionString)
        {
            DataContext dc = new DataContext(connectionString);
            categoriesTable = dc.GetTable<Category>();
        }

        public Category FetchByID(int categoryID)
        {
            return categoriesTable.First(c => c.CategoryID == categoryID);
        }

        public IQueryable<Category> Categories { get { return categoriesTable; } }

        public void Add(Category category)
        {
            categoriesTable.InsertOnSubmit(category);
            categoriesTable.Context.SubmitChanges();
        }

        public void Save(Category category)
        {
            if (!categoriesTable.Any(m => m.CategoryID == category.CategoryID))
                this.Add(category);
            else
            {
                Category cat = categoriesTable.Single(c => c.CategoryID == category.CategoryID);
                cat.CategoryName = category.CategoryName;
            }
            categoriesTable.Context.SubmitChanges();
        }

        public void Delete(Category category)
        {
            categoriesTable.DeleteOnSubmit(category);
            categoriesTable.Context.SubmitChanges();
        }
    }
}