using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class ProductCategoryDAO
    {
        OnlineShopDbContext db;

        public ProductCategoryDAO()
        {
            db = new OnlineShopDbContext();
        }

        public int AddProductCategory(ProductCategory productCategory)
        {
            productCategory.CreatedDate = DateTime.Now;
            db.ProductCategories.Add(productCategory);
            db.SaveChanges();
            return productCategory.ID;
        }

        public ProductCategory GetProductCategoryByID(int id)
        {
            return db.ProductCategories.Find(id);
        }

        public bool Update(ProductCategory p)
        {
            try
            {
                var productCategory = db.ProductCategories.Find(p.ID);
                productCategory.UpdatedDate = DateTime.Now;
                productCategory.Name = p.Name;
                productCategory.Status = p.Status;
                productCategory.DisplayOrder = p.DisplayOrder;
                productCategory.ParentID = p.ParentID;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool DeleteProductCategory(int id)
        {
            try
            {
                var productCategory = db.ProductCategories.Find(id);
                db.ProductCategories.Remove(productCategory);
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
