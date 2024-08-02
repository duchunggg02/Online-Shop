using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;

namespace Model.DAO
{
    public class UserDAO
    {
        OnlineShopDbContext db;

        public UserDAO()
        {
            db = new OnlineShopDbContext();
        }

        public IEnumerable<User> ListUsers(int page, int pageSize)
        {
            return db.Users.OrderBy(u => u.CreatedDate).ToPagedList(page, pageSize);
        }

        public int AddUser(User user)
        {
            db.Users.Add(user);
            db.SaveChanges();
            return user.ID;
        }

        public bool IsUserNameExist(string userName)
        {
            //var result = db.Users.Count(x => x.UserName == userName);
            //if (result > 0)
            //{
            //    return true;
            //}
            //else
            //{
            //    return false;
            //}
            return db.Users.Any(u => u.UserName == userName); //dừng ngay sau khi tìm thấy 1 kết quả
        }

        public bool Login(string userName, string passWord)
        {
            var result = db.Users.Count(x => x.UserName == userName && x.Password == passWord);
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public User GetUserByID(int id)
        {
            return db.Users.Find(id);
        }

        public User GetUserByName(string userName)
        {
            return db.Users.SingleOrDefault(x => x.UserName == userName);
        }
    }
}
