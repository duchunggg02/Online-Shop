using Model.DAO;
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
    }
}