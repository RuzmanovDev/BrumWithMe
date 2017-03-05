using BrumWithMe.MVC.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BrumWithMe.MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "UsersProfiles",
                url: "users/{username}",
                defaults: new
                {
                    controller = nameof(HomeController),
                    action = nameof(HomeController.Profiles),
                    username = UrlParameter.Optional
                });

            routes.MapRoute(
              name: "TestHomePage",
              url: "home",
              defaults: new
              {
                  controller = "Home",
                  action = nameof(HomeController.TestHomePage),
              });

            routes.MapRoute(
              name: "TripDetails",
              url: "trip/{action}/{id}",
              defaults: new { controller = "Trip", action = "TripDetails", id = UrlParameter.Optional }

          );

            routes.MapRoute(
            name: "GetTownsApi",
            url: "api/trip/{action}",
            defaults: new { controller = "Trip", action = "GetTowns" }
        );

            routes.MapRoute(
               name: "Default",
               url: "{controller}/{action}/{id}",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }

           );

            routes.MapRoute(
            name: "Comments",
            url: "comments/{controller}/{action}/{page}",
            defaults: new
            {
                controller = "Home",
                action = nameof(HomeController.LoadComments),
                page = UrlParameter.Optional
            });

        }
    }
}
