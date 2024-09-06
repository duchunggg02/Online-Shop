using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class OrderDetailDAO
    {
        OnlineShopDbContext db;

        public OrderDetailDAO()
        {
            db = new OnlineShopDbContext();
        }

        public bool Insert(OrderDetail orderDetail)
        {
            try
            {
                db.OrderDetails.Add(orderDetail);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<OrderDetail> GetOrderDetail(int orderId)
        {
            return db.OrderDetails.Where(o => o.OrderID == orderId).ToList();
        }
    }
}
