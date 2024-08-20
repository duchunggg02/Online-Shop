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
            var cartSession = Session[Cart.CartSession];
            var list = new List<CartItem>();
            if (cartSession != null)
            {
                list = (List<CartItem>)cartSession;
            }
            return View(list);
        }

        public ActionResult AddItem(int productId, int quantity)
        {
            var product = new ProductDAO().GetProductById(productId);

            var session = Session[Cart.CartSession];

            if (session != null)
            {
                var list = (List<CartItem>)session;
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
                    var item = new CartItem();
                    item.Product = product;
                    item.Quantity = quantity;
                    list.Add(item);
                }
            }
            else
            {
                var item = new CartItem();
                item.Product = product;
                item.Quantity = quantity;
                var list = new List<CartItem>();
                list.Add(item);

                //lưu vào session
                Session[Cart.CartSession] = list;
            }
            return RedirectToAction("Index");
        }

        public JsonResult Update(string cartModel)
        {
            //chuyển json thành danh sách cart item
            var jsonCart = new JavaScriptSerializer().Deserialize<List<CartItem>>(cartModel);

            //lấy giỏ hàng từ session
            var sessionCart = (List<CartItem>)Session[Cart.CartSession];

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
            Session[Cart.CartSession] = null;
            return Json(new
            {
                status = true
            });
        }

        public JsonResult Delete(int id)
        {
            //lấy giỏ hàng từ session
            var sessionCart = (List<CartItem>)Session[Cart.CartSession];

            sessionCart.RemoveAll(x => x.Product.ID == id);
            Session[Cart.CartSession] = sessionCart;

            return Json(new
            {
                status = true
            });
        }

        [HttpGet]
        public ActionResult Payment()
        {
            var cartSession = Session[Cart.CartSession];
            var list = new List<CartItem>();
            if (cartSession != null)
            {
                list = (List<CartItem>)cartSession;
            }
            return View(list);
        }

        [HttpPost]
        public ActionResult Payment(int status)
        {
            var order = new Order();
            order.CreatedDate = DateTime.Now;
            order.Status = status;

            try
            {
                var id = new OrderDAO().Insert(order);
                var cart = (List<CartItem>)Session[Cart.CartSession];
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
            }
            catch (Exception ex)
            {

            }
            return Redirect("Index");
        }
    }
}