using Model.DAO;
using Online_Shop.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Online_Shop.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order

        
        public ActionResult Index()
        {
            var orderDao = new OrderDAO();
            var userSession = (UserLogin)Session["UserLogin"];
            var orders = orderDao.ListOrder(userSession.Id);
            return View(orders);
        }


        public ActionResult OrderDetail(int orderId)
        {
            var orderDetailDao = new OrderDetailDAO();
            var orderDetail = orderDetailDao.GetOrderDetail(orderId);

            if (orderDetail == null)
            {
                return HttpNotFound();
            }

            return View(orderDetail);
        }
    }
}