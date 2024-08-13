using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Shop.Controllers
{
    public class ProductController : Controller
    {
        // GET: ProductC
        public ActionResult Index()
        {
            return View();
        }

        [ChildActionOnly]
        public PartialViewResult ProductCategory()
        {
            var model = new ProductCategoryDAO().ListAll();
            return PartialView(model);
        }

        public ActionResult CategoryDetail(int id)
        {
            var productCategory = new ProductCategoryDAO().GetProductCategoryByID(id);
            return View(productCategory);
        }

        public ActionResult ProductDetail(int id)
        {
            var product = new ProductDAO().GetProductById(id);
            ViewBag.ProductCategory = new ProductCategoryDAO().GetProductCategoryByID(product.ProductCategoryID);
            ViewBag.RelatedProduct = new ProductDAO().ListRelatedProduct(id);
            return View(product);
        }
    }
}