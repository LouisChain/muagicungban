using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Linq;

using muagicungban.Models;
using muagicungban.Abstract;

namespace muagicungban.Repositories
{
    public class SubCategoriesRepository : ISubCategoriesRepository
    {
        private Table<SubCategory> subCategoriesTable;
        public SubCategoriesRepository(string connectionString)
        {
            DataContext dc = new DataContext(connectionString);
            subCategoriesTable = dc.GetTable<SubCategory>();
        }

        public SubCategory FetchByID(int ID)
        {
            return subCategories.First(s => s.CategoryID == ID);
        }

        public IQueryable<SubCategory> subCategories { get { return subCategoriesTable; } }

        public void Add(SubCategory subCategory)
        {
            subCategoriesTable.InsertOnSubmit(subCategory);
            subCategoriesTable.Context.SubmitChanges();
        }

        public void Save(SubCategory subCategory)
        {
            if (!subCategoriesTable.Any(s => s.ID == subCategory.ID))
                this.Add(subCategory);
            else
            {
                SubCategory subCat = subCategoriesTable.Single(s => s.ID == subCategory.ID);
                subCat.Name = subCategory.Name;
                subCat.CategoryID = subCategory.CategoryID;
            }
            subCategoriesTable.Context.SubmitChanges();
        }

        public void Delete(SubCategory subCategory)
        {
            subCategoriesTable.DeleteOnSubmit(subCategory);
            subCategoriesTable.Context.SubmitChanges();
        }
    }
}