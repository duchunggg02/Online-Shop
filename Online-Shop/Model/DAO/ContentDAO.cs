using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class ContentDAO
    {
        OnlineShopDbContext db;

        public ContentDAO()
        {
            db = new OnlineShopDbContext();
        }

        public IEnumerable<Content> ListContents(string search, int page, int pageSize)
        {
            IQueryable<Content> contents = db.Contents;
            if (!String.IsNullOrEmpty(search))
            {
                contents = contents.Where(c => c.Name.Contains(search));
            }
            return contents.OrderBy(c => c.Name).ToPagedList(page, pageSize);
        }

        public int AddContent(Content content)
        {
            content.CreatedDate = DateTime.Now;
            db.Contents.Add(content);
            db.SaveChanges();
            return content.ID;
        }

        public bool DeleteContent(int id)
        {
            try
            {
                var content = db.Contents.Find(id);
                db.Contents.Remove(content);
                db.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public Content GetContentByID(int id)
        {
            return db.Contents.Find(id);
        }

        public bool UpdateContent(Content c)
        {
            try
            {
                var content = db.Contents.Find(c.ID);
                content.UpdatedDate = DateTime.Now;
                content.Name = c.Name;
                content.Description = c.Description;
                content.Image = c.Image;
                content.CategoryID = c.CategoryID;
                content.Detail = c.Detail;
                content.Status = c.Status;
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
            var content = db.Contents.Find(id);
            content.Status = !content.Status;
            db.SaveChanges();
            return content.Status;
        }
    }
}