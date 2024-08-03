using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.DAO;
using Model.EF;
using Online_Shop.Common;
using PagedList;

namespace Online_Shop.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        // GET: Admin/User
        public ActionResult Index(int page = 1, int pageSize = 1)
        {
            var dao = new UserDAO();

            var list = dao.ListUsers(page, pageSize);

            return View(list);
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Create(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();

                if (!dao.IsUserNameExist(user.UserName)) //kiểm tra tên đăng nhập đã tồn tại chưa
                {
                    var passEncrypt = Encryptor.GetMd5Hash(user.Password);
                    user.Password = passEncrypt;

                    int id = dao.AddUser(user);
                    if (id > 0) //thêm thành công
                    {
                        return RedirectToAction("Index", "User");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Thêm thất bại!!");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại!!");
                }
            }
            return View("Create");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var user = new UserDAO().GetUserByID(id);
            return View(user);
        }

        [HttpPost]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();

                var passEncrypt = Encryptor.GetMd5Hash(user.Password);
                user.Password = passEncrypt;

                bool result = dao.UpdateUser(user);
                if (result) //cập nhật thành công
                {
                    return RedirectToAction("Index", "User");
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
            new UserDAO().DeleteUser(id);
            return RedirectToAction("Index", "User");
        }
    }
}