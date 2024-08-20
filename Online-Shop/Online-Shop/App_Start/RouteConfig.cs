using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Online_Shop
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Register",
                url: "dang-ky-thanh-vien",
                defaults: new { controller = "User", action = "Register", id = UrlParameter.Optional },
                namespaces: new[] { "Online_Shop.Controllers" }
                );

            routes.MapRoute(
             name: "Login",
             url: "dang-nhap",
             defaults: new { controller = "User", action = "Login", id = UrlParameter.Optional },
             namespaces: new[] { "Online_Shop.Controllers" }
             );

            routes.MapRoute(
            name: "Default",
            url: "{controller}/{action}/{id}",
            defaults: new
            {
                controller = "Home",
                action = "Index",
                id = UrlParameter.Optional
            },
              namespaces: new[]
                {
                      "Online_Shop.Controllers"
                }
        );
        }
    }
}
