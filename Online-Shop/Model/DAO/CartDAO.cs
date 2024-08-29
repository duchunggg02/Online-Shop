using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class CartDAO
    {
        OnlineShopDbContext db;

        public CartDAO()
        {
            db = new OnlineShopDbContext();
        }

        //khởi tạo giỏ hàng
        public Cart CreateCart(int userId)
        {
            var cart = new Cart
            {
                UserID = userId,
                CreatedDate = DateTime.Now,
                Status = true
            };
            db.Carts.Add(cart);
            db.SaveChanges();
            return cart;
        }

        public Cart GetCartByUserId(int userId)
        {
            return db.Carts.FirstOrDefault(c => c.UserID == userId);
        }

        public void ClearCart(int userId)
        {
            var cartItems = db.Carts.Where(c => c.UserID == userId);
            db.Carts.RemoveRange(cartItems);
            db.SaveChanges();
        }
    }
}
