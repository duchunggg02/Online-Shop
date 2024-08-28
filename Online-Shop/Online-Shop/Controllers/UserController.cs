using CloudinaryDotNet.Actions;
using CloudinaryDotNet;
using Model.DAO;
using Model.EF;
using Online_Shop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Online_Shop.Common;

namespace Online_Shop.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(RegisterModel model, HttpPostedFileBase file)
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

                    model.Image = uploadResult.SecureUrl.ToString();
                }

                var dao = new UserDAO();
                if (dao.IsUserNameExist(model.UserName))
                {
                    ModelState.AddModelError("", "Tên đăng nhập đã tồn tại!!");
                }
                else if (dao.IsEmailExist(model.Email))
                {
                    ModelState.AddModelError("", "Email đã tồn tại!!");
                }
                else
                {
                    var user = new User();
                    user.UserName = model.UserName;

                    var passEncrypt = Encryptor.GetMd5Hash(model.Password);
                    user.Password = passEncrypt;

                    user.LastName = model.LastName;
                    user.FirstName = model.FirstName;
                    user.Email = model.Email;
                    user.Phone = model.Phone;
                    user.Address = model.Address;
                    user.Image = model.Image;
                    user.Status = true;
                    var result = dao.AddUser(user);
                    if (result > 0)
                    {
                        ViewBag.Success = "Đăng ký thành công!!";
                        model = new RegisterModel();
                    }
                    else
                    {
                        ModelState.AddModelError("", "Đăng ký không thành công!!");
                    }
                }

            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
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
                    userSession.Name = user.FirstName;
                    userSession.Image = user.Image;
                    Session.Add("UserLogin", userSession);// thêm UserLogin vào session

                    //gộp giỏ hàng trong session vào giỏ hàng trong database
                    var cartSession = Session[CartSession.Session] as List<CartItemSession>;

                    if (cartSession != null && cartSession.Any())
                    {
                        var cartDao = new CartDAO();
                        var cartDetailDao = new CartDetailDAO();
                        var cart = cartDao.GetCartByUserId(userSession.Id);

                        if(cart == null)
                        {
                            cart = cartDao.CreateCart(userSession.Id);
                        }

                        foreach(var item in cartSession)
                        {
                            cartDetailDao.AddItem(cart.ID, item.Product.ID, item.Quantity);
                        }


                        //xóa giỏ hàng trong session sau khi gộp
                        Session[CartSession.Session] = null;
                    }

                    return Redirect("/");
                }
                else
                {
                    ModelState.AddModelError("", "Tên đăng nhập hoặc mật khẩu không đúng");
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            Session["UserLogin"] = null;
            return Redirect("/");
        }
    }
}