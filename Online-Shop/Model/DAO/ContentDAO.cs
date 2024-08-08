using Model.EF;
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