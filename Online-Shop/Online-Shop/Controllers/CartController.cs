using Model.DAO;
using Model.EF;
using Online_Shop.Common;
using Online_Shop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
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
                        ProductID = c.ProductID,
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
                        ProductID = c.Product.ID,
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
            //chuyển json thành danh sách cart item
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItemSession>>(cartModel);

            //lấy giỏ hàng từ session
            var sessionCart = (List<CartItemSession>)Session[CartSession.Session];

            foreach (var item in sessionCart)
            {
                var jsonItem = jsonCart.SingleOrDefault(x => x.Product.ID == item.Product.ID);
                if (jsonItem != null)
                {
                    item.Quantity = jsonItem.Quantity;
                }
            }
            return Json(new
            {
                status = true
            });
        }

        public JsonResult DeleteAll()
        {
            Session[CartSession.Session] = null;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Delete(int id)
        {
            //lấy giỏ hàng từ session
            var sessionCart = (List<CartItemSession>)Session[CartSession.Session];

            sessionCart.RemoveAll(x => x.Product.ID == id);
            Session[CartSession.Session] = sessionCart;

            return Json(new
            {
                status = true
            });
        }

        [HttpGet]
        public ActionResult Payment()
        {
            var cartSession = Session[CartSession.Session];
            var list = new List<CartItemSession>();
            if (cartSession != null)
            {
                list = (List<CartItemSession>)cartSession;
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult Payment(int status)
        {
            //kiem tra dang nhap
            var userSession = (UserLogin)Session["UserLogin"];
            if (userSession == null)
            {
                return RedirectToAction("Login", "User");
            }

            var order = new Order();
            order.CreatedDate = DateTime.Now;
            order.Status = status;
            order.CustomerID = userSession.Id;

            try
            {
                var cartDAO = new CartDAO();

                var id = new OrderDAO().Insert(order);
                var cart = (List<CartItemSession>)Session[CartSession.Session];
                var detail = new OrderDetailDAO();
                foreach (var item in cart)
                {
                    var orderDetail = new OrderDetail();
                    orderDetail.OrderID = id;
                    orderDetail.ProductID = item.Product.ID;
                    orderDetail.Quantity = item.Quantity;
                    orderDetail.Price = item.Product.Price;
                    detail.Insert(orderDetail);
                }

                cartDAO.ClearCart(userSession.Id);
            }
            catch (Exception ex)
            {

            }
            return Redirect("Index");
        }
    }
}