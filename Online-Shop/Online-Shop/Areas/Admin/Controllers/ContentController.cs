using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Model.DAO;
using Model.EF;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Shop.Areas.Admin.Controllers
{
    public class ContentController : BaseController
    {
        // GET: Admin/Content
        public ActionResult Index(string search, int page = 1, int pageSize = 10)
        {
            var dao = new ContentDAO();

            var list = dao.ListContents(search, page, pageSize);

            ViewBag.Search = search;

            return View(list);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Content content, HttpPostedFileBase file)
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

                    content.Image = uploadResult.SecureUrl.ToString();
                }

                var dao = new ContentDAO();

                int id = dao.AddContent(content);
                if (id > 0)
                {
                    SetAlert("Thêm tin tức thành công", "success");
                    return RedirectToAction("Index", "Content");
                }
                else
                {
                    ModelState.AddModelError("", "Thêm thất bại!!");
                }
            }

            return View("Create");
        }

        [HttpDelete]
        public ActionResult Delete(int id)
        {
            new ContentDAO().DeleteContent(id);
            return RedirectToAction("Index");
        }
    }
}