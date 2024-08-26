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
using static System.Collections.Specialized.BitVector32;

namespace Online_Shop.Controllers
{
    public class CartController : Controller
    {

        // GET: Cart
        public ActionResult Index()
        {
            var userSession = (UserLogin)Session["UserLogin"];
            var list = new List<CartItemSession>();
            if (userSession != null)
            {
                var cartItems = new CartDAO().GetCartByUserId(userSession.Id);
                var productDAO = new ProductDAO();

                list = cartItems.Select(c => new CartItemSession
                {
                    Product = productDAO.GetProductById(c.ProductID),
                    Quantity = c.Quantity
                }).ToList();
            }
            else
            {
                var cartSession = Session[CartSession.Session];
                if (cartSession != null)
                {
                    list = (List<CartItemSession>)cartSession;
                }
            }

            return View(list);
        }

        public ActionResult AddItem(int productId, int quantity)
        {
            var product = new ProductDAO().GetProductById(productId);

            var userSession = (UserLogin)Session["UserLogin"];

            if (userSession == null) // chưa đăng nhập
            {
                // lưu giỏ hàng vào session
                var session = Session[CartSession.Session];
                if(session != null)
                {
                    var list = (List<CartItemSession>)session;
                    if (list.Exists(c => c.Product.ID == productId))
                    {
                        foreach (var item in list)
                        {
                            if (item.Product.ID == productId)
                            {
                                item.Quantity += quantity;
                            }
                        }
                    }
                    else
                    {
                        var item = new CartItemSession();
                        item.Product = product;
                        item.Quantity = quantity;
                        list.Add(item);
                    }
                }
                else
                {
                    var item = new CartItemSession();
                    item.Product = product;
                    item.Quantity = quantity;
                    var list = new List<CartItemSession>();
                    list.Add(item);

                    //lưu vào session
                    Session[CartSession.Session] = list;
                }
            } 
            else // đã đăng nhập
            {
                var cart = new CartDAO();
                cart.AddItemToCart(userSession.Id, productId, quantity);
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