using Model.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
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
    }
}