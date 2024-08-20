using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PagedList;
using System.Data.Entity;

namespace Model.DAO
{
    public class UserDAO
    {
        OnlineShopDbContext db;

        public UserDAO()
        {
            db = new OnlineShopDbContext();
        }

        public IEnumerable<User> ListUsers(string search, int page, int pageSize)
        {
            IQueryable<User> users = db.Users;
            if (!String.IsNullOrEmpty(search))
            {
                users = users.Where(u => u.LastName.Contains(search) || u.FirstName.Contains(search));
            }
            return users.OrderBy(u => u.CreatedDate).ToPagedList(page, pageSize);
        }

        public int AddUser(User user)
        {
            user.CreatedDate = DateTime.Now;
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

        public bool IsEmailExist(string email)
        {
            return db.Users.Any(u => u.Email == email);
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

        public bool UpdateUser(User u)
        {
            try
            {
                var user = db.Users.Find(u.ID);
                user.LastName = u.LastName;
                user.FirstName = u.FirstName;
                user.Address = u.Address;
                user.Phone = u.Phone;
                user.Email = u.Email;
                user.Image = u.Image;
                //user.UpdatedBy = u.UpdatedBy;
                user.UpdatedDate = DateTime.Now;
                user.Status = u.Status;
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public bool DeleteUser(int id)
        {
            try
            {
                var user = db.Users.Find(id);
                db.Users.Remove(user);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool ChangeStatus(int id)
        {
            var user = db.Users.Find(id);
            user.Status = !user.Status;
            db.SaveChanges();
            return user.Status;
        }
    }
}
