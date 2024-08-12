using Model.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.DAO
{
    public class MenuDAO
    {
        OnlineShopDbContext db = null;

        public MenuDAO()
        {
            db = new OnlineShopDbContext();
        }

        public List<Menu> ListByGroupID(int id)
        {
            return db.Menus.Where(x => x.MenuTypeID == id && x.Status == true).OrderBy(x => x.DisplayOrder).ToList();
        }
    }
}
