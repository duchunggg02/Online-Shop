using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Online_Shop.Common
{
    [Serializable]
    public class UserLogin
    {
        public int Id { get; set; }
        public string UserName { get; set; }
    }
}