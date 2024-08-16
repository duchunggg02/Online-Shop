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

        public IEnumerable<Product> ListProducts(string search, int page, int pageSize)
        {
            IQueryable<Product> products = db.Products;
            if (!String.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.Name.Contains(search) || p.Code.Contains(search));
            }
            return products.OrderBy(p => p.Name).ToPagedList(page, pageSize);
        }

        public int AddProduct(Product product)
        {
            product.CreatedDate = DateTime.Now;
            product.ViewCount = 0;
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
                product.ProductCategoryID = p.ProductCategoryID;
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

        public IEnumerable<Product> SearchProduct(string search)
        {
            var products = from p in db.Products
                           select p;

            if (!String.IsNullOrEmpty(search))
            {
                products = products.Where(p => p.Name.Contains(search));
            }

            return products.ToList();
        }

        public bool ChangeStatus(int id)
        {
            var product = db.Products.Find(id);
            product.Status = !product.Status;
            db.SaveChanges();
            return product.Status;
        }

        /// <summary>
        /// sản phẩm mới
        /// </summary>
        /// <param name="top"></param>
        /// <returns></returns>
        public List<Product> ListNewProduct(int top)
        {
            return db.Products.OrderByDescending(p => p.CreatedDate).Take(top).ToList();
        }

        /// <summary>
        /// lấy sản phẩm liên quan
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Product> ListRelatedProduct(int id)
        {
            var product = db.Products.Find(id);
            return db.Products.Where(p => p.ID != id && p.ProductCategoryID == product.ProductCategoryID).Take(4).ToList();
        }

        /// <summary>
        /// lấy sản phẩm theo category id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public List<Product> ListProductByCateID(int id, ref int totalProduct, int page, int pageSize)
        {
            totalProduct = db.Products.Where(p => p.ProductCategoryID == id).Count();
            var product = db.Products.Where(p => p.ProductCategoryID == id).OrderByDescending(p => p.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize).ToList();
            return product;
        }

        public void ViewCountProduct(Product product)
        {
            try
            {
                var prod = db.Products.Find(product.ID);
                prod.ViewCount += 1;
                db.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
