using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
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

        public IEnumerable<ProductCategory> ListProductCategories(string search, int page, int pageSize)
        {
            IQueryable<ProductCategory> productCategories = db.ProductCategories;
            if (!String.IsNullOrEmpty(search))
            {
                productCategories = productCategories.Where(p => p.Name.Contains(search));
            }
            return productCategories.OrderBy(p => p.Name).ToPagedList(page, pageSize);
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

        public bool ChangeStatus(int id)
        {
            var productCategory = db.ProductCategories.Find(id);
            productCategory.Status = !productCategory.Status;
            db.SaveChanges();
            return productCategory.Status;
        }

        public List<ProductCategory> ListAll()
        {
            return db.ProductCategories.Where(p => p.Status == true).OrderBy(p => p.DisplayOrder).ToList();
        }

        //public ProductCategory ViewDetail(int id)
        //{
        //    return db.ProductCategories.Find(id);
        //} 
    }
}
