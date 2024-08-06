using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_Shop.Common
{
    public static class UserHelper
    {
        public static UserLogin GetLoginUser()
        {
            return HttpContext.Current.Session["UserLogin"] as UserLogin;
        }
    }
}