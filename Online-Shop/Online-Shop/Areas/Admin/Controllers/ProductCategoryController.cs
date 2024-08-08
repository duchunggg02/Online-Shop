using Model.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.EF;

namespace Online_Shop.Areas.Admin.Controllers
{
    public class ProductCategoryController : BaseController
    {
        // GET: Admin/ProductCategory
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductCategoryDAO();

                int id = dao.AddProductCategory(productCategory);
                if (id > 0)
                {
                    SetAlert("Thêm danh mục sản phẩm thành công", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm thất bại!!");
                }
            }
            return View("Create");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var productCategory = new ProductCategoryDAO().GetProductCategoryByID(id);
            return View(productCategory);
        }

        [HttpPost]
        public ActionResult Edit(ProductCategory productCategory)
        {
            if (ModelState.IsValid)
            {
                var dao = new ProductCategoryDAO();

                var result = dao.Update(productCategory);
                if (result)
                {
                    SetAlert("Cập nhật danh mục sản phẩm thành công", "success");
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thất bại!!");
                }
            }
            return View("Edit");
        }
    }
}