using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PagedList;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class ProductDAO
    {
        OnlineShopDbContext db;

        public ProductDAO()
        {
            db = new OnlineShopDbContext();
        }

        public IEnumerable<Product> ListProducts(int page, int pageSize)
        {
            return db.Products.OrderBy(p => p.Name).ToPagedList(page, pageSize);
        }

        public int AddProduct(Product product)
        {
            product.CreatedDate = DateTime.Now;
            db.Products.Add(product);
            db.SaveChanges();
            return product.ID;
        }

        public bool UpdateProduct(Product p)
        {
            try
            {
                var product = db.Products.Find(p.ID);
                product.Name = p.Name;
                product.Code = p.Code;
                product.UpdatedDate = DateTime.Now;
                product.Description = p.Description;
                product.Price = p.Price;
                product.Image = p.Image;
                product.PromotionPrice = p.PromotionPrice;
                product.Quantity = p.Quantity;
                product.Detail = p.Detail;
                product.CategoryID = p.CategoryID;
                product.Status = p.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Product GetProductById(int id)
        {
            return db.Products.Find(id);
        }

        public bool DeleteProduct(int id)
        {
            try
            {
                var product = db.Products.Find(id);
                db.Products.Remove(product);
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
