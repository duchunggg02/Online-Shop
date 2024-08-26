using Model.Common;
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

        public void AddItemToCart(int userId, int productId, int quantity)
        {
            var cart = db.Carts.FirstOrDefault(c => c.UserID == userId && c.ProductID == productId);
            if (cart != null)
            {
                cart.Quantity += quantity;
            }
            else
            {
                cart = new Cart();
                cart.UserID = userId;
                cart.ProductID = productId;
                cart.Quantity = quantity;
                db.Carts.Add(cart);
            }
            db.SaveChanges();
        }

        public List<CartItem> GetCartByUserId(long userId)
        {
            return db.Carts
                .Where(c => c.UserID == userId)
                .Select(c => new CartItem
                {
                    ProductID = c.ProductID,
                    Quantity = c.Quantity
                })
                .ToList();
        }

        public void ClearCart(long userId)
        {
            var cartItems = db.Carts.Where(c => c.UserID == userId);
            db.Carts.RemoveRange(cartItems);
            db.SaveChanges();
        }
    }
}
