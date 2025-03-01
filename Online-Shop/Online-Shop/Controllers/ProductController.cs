﻿using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;

namespace Online_Shop.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
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

        public ActionResult Product(string categorySlug, int page = 1, int pageSize = 4, string sort = "")
        {
            //productCategory = new ProductCategoryDAO().GetProductCategoryByID(id);
            var productCategory = new ProductCategoryDAO().GetProductCategoryBySlug(categorySlug);
            ViewBag.Category = productCategory;

            int totalProduct = 0;
            var product = new ProductDAO().ListProductByCateID(productCategory.ID, ref totalProduct, page, pageSize, sort);

            ViewBag.TotalProduct = totalProduct;
            ViewBag.Page = page;

            int maxPage = 5;
            int totalPage = (int)Math.Ceiling((double)(totalProduct / pageSize));

            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            ViewBag.Sort = sort;

            return View(product);
        }

        public ActionResult Search(string keyword, int page = 1, int pageSize = 4)
        {
            int totalProduct = 0;
            var product = new ProductDAO().Search(keyword, ref totalProduct, page, pageSize);

            ViewBag.Keyword = keyword;

            ViewBag.TotalProduct = totalProduct;
            ViewBag.Page = page;

            int maxPage = 5;
            int totalPage = (int)Math.Ceiling((double)(totalProduct / pageSize));

            ViewBag.TotalPage = totalPage;
            ViewBag.MaxPage = maxPage;
            ViewBag.First = 1;
            ViewBag.Last = totalPage;
            ViewBag.Next = page + 1;
            ViewBag.Prev = page - 1;

            return View(product);
        }

        public ActionResult ProductDetail(string productSlug)
        {
            //var product = new ProductDAO().GetProductById(id);
            var product = new ProductDAO().GetProductBySlug(productSlug);
            UpdateViewCount(product);
            ViewBag.ProductCategory = new ProductCategoryDAO().GetProductCategoryByID(product.ProductCategoryID);
            ViewBag.RelatedProduct = new ProductDAO().ListRelatedProduct(product.ID);
            return View(product);
        }

        public void UpdateViewCount(Product product)
        {
            new ProductDAO().ViewCountProduct(product);
        }

        public JsonResult ListName(string keyword)
        {
            var listName = new ProductDAO().ListName(keyword);
            return Json(
                new
                {
                    data = listName,
                    status = true
                }, JsonRequestBehavior.AllowGet);
        }
    }
}