using Model.DAO;
using Model.EF;
using Online_Shop.Common;
using Online_Shop.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Online_Shop.Controllers
{
    public class CartController : Controller
    {

        // GET: Cart
        public ActionResult Index()
        {
            var userSession = (UserLogin)Session["UserLogin"];
            //var list = new List<CartItemSession>();

            var cartView = new List<CartViewModel>();

            var cartDetailDAO = new CartDetailDAO();
            var productDAO = new ProductDAO();

            if (userSession != null)
            {
                //đã đăng nhập, lấy giỏ hàng từ database
                var cart = new CartDAO().GetCartByUserId(userSession.Id);

                if (cart != null)
                {
                    var cartDetail = cartDetailDAO.GetCartItems(cart.ID);

                    cartView = cartDetail.Select(c => new CartViewModel
                    {
                        ID = c.ProductID,
                        ProductName = productDAO.GetProductById(c.ProductID).Name,
                        ProductImage = productDAO.GetProductById(c.ProductID).Image,
                        ProductPrice = productDAO.GetProductById(c.ProductID).Price,
                        Quantity = c.Quantity
                    }).ToList();
                }

            }
            else
            {
                //chưa đăng nhập, lấy giỏ hàng từ session
                var cartSession = Session[CartSession.Session] as List<CartItemSession>;
                if (cartSession != null)
                {
                    cartView = cartSession.Select(c => new CartViewModel
                    {
                        ID = c.Product.ID,
                        ProductName = c.Product.Name,
                        ProductImage = c.Product.Image,
                        ProductPrice = c.Product.Price,
                        Quantity = c.Quantity
                    }).ToList();
                }
            }

            return View(cartView);
        }

        public ActionResult AddItem(int productId, int quantity)
        {
            var product = new ProductDAO().GetProductById(productId);

            if (product == null)
            {
                return RedirectToAction("Index");
            }

            var userSession = (UserLogin)Session["UserLogin"];

            if (userSession == null) // chưa đăng nhập
            {
                // lưu giỏ hàng vào session
                var cartItems = Session[CartSession.Session] as List<CartItemSession>;
                if (cartItems != null)
                {
                    var existingItem = cartItems.FirstOrDefault(c => c.Product.ID == productId);
                    if (existingItem != null)
                    {
                        existingItem.Quantity += quantity;
                    }
                    else
                    {
                        cartItems.Add(new CartItemSession
                        {
                            Product = product,
                            Quantity = quantity
                        });
                    }
                }
                else
                {
                    cartItems = new List<CartItemSession>
                    {
                        new CartItemSession
                        {
                            Product = product,
                            Quantity = quantity
                        }
                    };
                }
                Session[CartSession.Session] = cartItems;
            }
            else // đã đăng nhập
            {
                var cart = new CartDAO().GetCartByUserId(userSession.Id);
                if (cart == null)
                {
                    cart = new CartDAO().CreateCart(userSession.Id);
                }

                new CartDetailDAO().AddItem(cart.ID, productId, quantity);
            }

            return RedirectToAction("Index");
        }

        public JsonResult Update(string cartModel)
        {
            var userSession = (UserLogin)Session["UserLogin"];
            var cartDao = new CartDAO();
            var cartDetailDao = new CartDetailDAO();

            //chuyển json thành danh sách cart item
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartViewModel>>(cartModel);

            if (userSession != null)
            {
                var cart = cartDao.GetCartByUserId(userSession.Id);
                if (cart != null)
                {
                    foreach (var item in jsonCart)
                    {
                        cartDetailDao.UpdateItem(cart.ID, item.ID, item.Quantity);
                    }
                }
            }
            else
            {
                //lấy giỏ hàng từ session
                var sessionCart = (List<CartItemSession>)Session[CartSession.Session];
                if (sessionCart != null)
                {
                    foreach (var item in sessionCart)
                    {
                        var jsonItem = jsonCart.SingleOrDefault(x => x.ID == item.Product.ID);
                        if (jsonItem != null)
                        {
                            item.Quantity = jsonItem.Quantity;
                        }
                    }
                    Session[CartSession.Session] = sessionCart;
                }
            }

            return Json(new
            {
                status = true
            });
        }

        public JsonResult DeleteAll()
        {
            var userSession = (UserLogin)Session["UserLogin"];
            var cartDao = new CartDAO();
            var cartDetailDao = new CartDetailDAO();

            if (userSession != null)
            {
                var cart = cartDao.GetCartByUserId(userSession.Id);
                if (cart != null)
                {
                    cartDetailDao.ClearCartDetail(cart.ID);
                    cartDao.ClearCart(userSession.Id);
                }
            }
            else
            {
                Session[CartSession.Session] = null;
            }

            return Json(new
            {
                status = true
            });
        }

        public JsonResult Delete(int id)
        {
            var userSession = (UserLogin)Session["UserLogin"];
            var cartDao = new CartDAO();
            var cartDetailDao = new CartDetailDAO();

            if (userSession != null)
            {
                var cart = cartDao.GetCartByUserId(userSession.Id);
                if (cart != null)
                {
                    cartDetailDao.DeleteItem(cart.ID, id);
                }
            }
            else
            {
                //lấy giỏ hàng từ session
                var sessionCart = (List<CartItemSession>)Session[CartSession.Session];
                if (sessionCart != null)
                {
                    sessionCart.RemoveAll(x => x.Product.ID == id);
                    Session[CartSession.Session] = sessionCart;
                }
            }

            return Json(new
            {
                status = true
            });
        }

        [HttpGet]
        public ActionResult Payment()
        {
            var userSession = (UserLogin)Session["UserLogin"];
            var cartDao = new CartDAO();
            var cartDetailDao = new CartDetailDAO();

            var cartView = new List<CartViewModel>();
            var productDAO = new ProductDAO();

            if (userSession == null)
            {
                return RedirectToAction("Login", "User");
            }

            var cart = cartDao.GetCartByUserId(userSession.Id);
            if (cart != null)
            {
                var cartDetail = cartDetailDao.GetCartItems(cart.ID);
                cartView = cartDetail.Select(c => new CartViewModel
                {
                    ID = c.ProductID,
                    ProductName = productDAO.GetProductById(c.ProductID).Name,
                    ProductImage = productDAO.GetProductById(c.ProductID).Image,
                    ProductPrice = productDAO.GetProductById(c.ProductID).Price,
                    Quantity = c.Quantity
                }).ToList();
            }

            return View(cartView);
        }

        [HttpPost]
        public ActionResult Payment(int status)
        {
            var userSession = (UserLogin)Session["UserLogin"];

            var order = new Order();
            order.CreatedDate = DateTime.Now;
            order.Status = status;
            order.CustomerID = userSession.Id;

            try
            {
                var cartDAO = new CartDAO();
                var cartDetailDao = new CartDetailDAO();

                var orderId = new OrderDAO().Insert(order);

                var cart = cartDAO.GetCartByUserId(userSession.Id);

                if (cart != null)
                {
                    var cartDetail = cartDetailDao.GetCartItems(cart.ID);

                    foreach (var item in cartDetail)
                    {
                        var orderDetail = new OrderDetail();
                        orderDetail.OrderID = orderId;
                        orderDetail.ProductID = item.ProductID;
                        orderDetail.Quantity = item.Quantity;

                        new OrderDetailDAO().Insert(orderDetail);
                    }
                }

                cartDetailDao.ClearCartDetail(cart.ID);
                cartDAO.ClearCart(userSession.Id);
                
            }
            catch (Exception ex)
            {

            }
            return Redirect("Index");
        }
    }
}