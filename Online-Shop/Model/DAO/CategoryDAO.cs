using Model.EF;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class CategoryDAO
    {
        OnlineShopDbContext db;
        public CategoryDAO()
        {
            db = new OnlineShopDbContext();
        }

        public int AddCategory(Category category)
        {
            category.CreatedDate = DateTime.Now;
            db.Categories.Add(category);
            db.SaveChanges();
            return category.ID;
        }

        public Category GetCategoryByID(int id)
        {
            return db.Categories.Find(id);
        }

        public bool Edit(Category c)
        {
            try
            {
                var category = db.Categories.Find(c.ID);
                category.Name = c.Name;
                category.Status = c.Status;
                category.UpdatedDate = DateTime.Now;
                category.ParentID = c.ParentID;
                category.DisplayOrder = c.DisplayOrder;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool DeleteCategory(int id)
        {
            try
            {
                var category = db.Categories.Find(id);
                db.Categories.Remove(category);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
