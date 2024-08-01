using Model.DAO;
using Online_Shop.Areas.Admin.Models;
using Online_Shop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Shop.Areas.Admin.Controllers
{
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                var dao = new UserDAO();
                var result = dao.Login(model.UserName, Encryptor.GetMd5Hash(model.Password));
                if (result)
                {
                    var user = dao.GetUserByName(model.UserName);
                    var userSession = new UserLogin();
                    userSession.UserName = user.UserName;
                    userSession.Id = user.ID;
                    Session.Add("UserLogin", userSession);// thêm UserLogin vào session
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng!!");
                }
            }
            else
            {
                ModelState.AddModelError("","Dữ liệu bị lỗi!!");
            }
            return View("Index");
        }
    }
}