using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class ContactDAO
    {
        OnlineShopDbContext db;

        public ContactDAO()
        {
            db = new OnlineShopDbContext();
        }

        public Contact GetContact()
        {
            return db.Contacts.Single(x => x.Status == true);
        }
    }
}
