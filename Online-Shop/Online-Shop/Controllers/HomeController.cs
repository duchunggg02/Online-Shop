using Model.DAO;
using Online_Shop.Models;
using System;
using System.Collections.Generic;
using System.EnterpriseServices;
using System.Linq;
using System.Security.AccessControl;
using System.Web;
using System.Web.Mvc;
using Online_Shop.Common;

namespace Online_Shop.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Slides = new SlideDAO().ListAll();
            ViewBag.NewProduct = new ProductDAO().ListNewProduct(4);
            return View();
        }

        [ChildActionOnly]
        public ActionResult MainMenu()
        {
            var model = new MenuDAO().ListByGroupID(2);
            return PartialView(model);
        }

        [ChildActionOnly]
        public ActionResult TopMenu()
        {
            var model = new MenuDAO().ListByGroupID(1);
            return PartialView(model);
        }


        [ChildActionOnly]
        public ActionResult Footer()
        {
            var model = new FooterDAO().GetFooter();
            return PartialView(model);
        }

        //[ChildActionOnly]
        //public ActionResult CartHeader()
        //{
        //    var cartSession = Session[CartSession.Session];
        //    var list = new List<CartItemSession>(); 
        //    var cartView = new List<CartViewModel>();
        //    if (cartSession != null)
        //    {
        //        list = (List<CartItemSession>)cartSession;
        //    }
          
        //    return PartialView(list);
        //}

        [ChildActionOnly]
        public ActionResult CartHeader()
        {
            var userSession = (UserLogin)Session["UserLogin"];
            var cartItems = new List<CartViewModel>();

            if (userSession != null) // Đã đăng nhập
            {
                var cart = new CartDAO().GetCartByUserId(userSession.Id);

                if (cart != null)
                {
                    var cartDetailDAO = new CartDetailDAO();
                    var productDAO = new ProductDAO();

                    var cartDetails = cartDetailDAO.GetCartItems(cart.ID);
                    cartItems = cartDetails.Select(c => new CartViewModel
                    {
                        ID = c.ProductID,
                        ProductName = productDAO.GetProductById(c.ProductID).Name,
                        ProductImage = productDAO.GetProductById(c.ProductID).Image,
                        ProductPrice = productDAO.GetProductById(c.ProductID).Price,
                        Quantity = c.Quantity
                    }).ToList();
                }
            }
            else // Chưa đăng nhập
            {
                var cartSession = Session[CartSession.Session] as List<CartItemSession>;
                if (cartSession != null)
                {
                    cartItems = cartSession.Select(c => new CartViewModel
                    {
                        ID = c.Product.ID,
                        ProductName = c.Product.Name,
                        ProductImage = c.Product.Image,
                        ProductPrice = c.Product.Price,
                        Quantity = c.Quantity
                    }).ToList();
                }
            }

            return PartialView(cartItems);
        }

    }
}