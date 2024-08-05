using Model.DAO;
using Model.EF;
using System;
using System.Buffers.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using System.Configuration;

namespace Online_Shop.Areas.Admin.Controllers
{
    public class ProductController : BaseController
    {
        // GET: Admin/Product
        public ActionResult Index(int page = 1, int pageSize = 10)
        {
            var dao = new ProductDAO();

            var listProducts = dao.ListProducts(page, pageSize);

            return View(listProducts);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product product, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    Account account = new Account(
                        ConfigurationManager.AppSettings["Cloudinary.CloudName"],
                        ConfigurationManager.AppSettings["Cloudinary.ApiKey"],
                        ConfigurationManager.AppSettings["Cloudinary.ApiSecret"]);

                    Cloudinary cloudinary = new Cloudinary(account);

                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, file.InputStream)
                    };

                    var uploadResult = cloudinary.Upload(uploadParams);

                    product.Image = uploadResult.SecureUrl.ToString();
                }

                var dao = new ProductDAO();
                int id = dao.AddProduct(product);
                if (id > 0) // thêm thành công
                {
                    return RedirectToAction("Index", "Product");
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
            var product = new ProductDAO().GetProductById(id);
            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(Product product, HttpPostedFileBase file)
        {
            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0)
                {
                    Account account = new Account(
                        ConfigurationManager.AppSettings["Cloudinary.CloudName"],
                        ConfigurationManager.AppSettings["Cloudinary.ApiKey"],
                        ConfigurationManager.AppSettings["Cloudinary.ApiSecret"]);

                    Cloudinary cloudinary = new Cloudinary(account);

                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.FileName, file.InputStream)
                    };

                    var uploadResult = cloudinary.Upload(uploadParams);

                    product.Image = uploadResult.SecureUrl.ToString();
                }

                var dao = new ProductDAO();

                var result = dao.UpdateProduct(product);
                if (result)
                {
                    return RedirectToAction("Index", "Product");
                }
                else
                {
                    ModelState.AddModelError("", "Cập nhật thất bại!!");
                }
            }
            return View("Edit");
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ProductDAO().DeleteProduct(id);
            return RedirectToAction("Index", "Product");
        }
    }
}