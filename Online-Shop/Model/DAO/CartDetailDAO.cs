using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class CartDetailDAO
    {
        OnlineShopDbContext db;

        public CartDetailDAO()
        {
            db = new OnlineShopDbContext();
        }

        public void AddItem(int cartId, int productId, int quantity)
        {
            var cartDetail = db.CartDetails.FirstOrDefault(c => c.CartID == cartId && c.ProductID == productId);

            if (cartDetail != null)
            {
                //sản phẩm đã tồn tại trong giỏ hàng, cập nhật số lượng
                cartDetail.Quantity += quantity;
            }
            else
            {
                //thêm sản phẩm mới vào giỏ hàng
                cartDetail = new CartDetail
                {
                    CartID = cartId,
                    ProductID = productId,
                    Quantity = quantity
                };
                db.CartDetails.Add(cartDetail);
            }
            db.SaveChanges();
        }

        public void UpdateItem(int cartId, int productId, int quantity)
        {
            var cartDetail = db.CartDetails.FirstOrDefault(cd => cd.CartID == cartId && cd.ProductID == productId);
            if (cartDetail != null)
            {
                cartDetail.Quantity = quantity;
                db.SaveChanges();
            }
        }

        public void DeleteItem(int cartId, int productId)
        {
            var cartDetail = db.CartDetails.FirstOrDefault(cd => cd.CartID == cartId && cd.ProductID == productId);
            if (cartDetail != null)
            {
                db.CartDetails.Remove(cartDetail);

                db.SaveChanges();

                //kiểm tra nếu không còn sản phẩm nào trong giỏ hàng thì xóa luôn giỏ hàng
                var remainingCartDetails = db.CartDetails.Where(x => x.CartID == cartId).Count();
                if (remainingCartDetails == 0)
                {
                    var cart = db.Carts.Where(x => x.ID == cartId);
                    db.Carts.RemoveRange(cart);
                    db.SaveChanges();
                }
            }
        }

        public void ClearCartDetail(int cartId)
        {
            var cartDetails = db.CartDetails.Where(cd => cd.CartID == cartId);
            db.CartDetails.RemoveRange(cartDetails);
            db.SaveChanges();
        }

        public List<CartDetail> GetCartItems(int cartId)
        {
            return db.CartDetails.Where(cd => cd.CartID == cartId).ToList();
        }
    }
}
