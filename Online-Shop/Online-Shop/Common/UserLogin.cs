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
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string Image { get; set; }
        public string Email { get; set; }
    }
}