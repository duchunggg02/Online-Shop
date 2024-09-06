using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class OrderDAO
    {
        OnlineShopDbContext db;

        public OrderDAO()
        {
            db = new OnlineShopDbContext();
        }

        public int Insert(Order order)
        {
            db.Orders.Add(order);
            db.SaveChanges();
            return order.ID;
        }

        // lấy danh sách đơn hàng của user
        public List<Order> ListOrder(int userId) 
        {
            return db.Orders.Where(o => o.CustomerID == userId).OrderByDescending(o => o.CreatedDate).ToList();
        }

    }
}
