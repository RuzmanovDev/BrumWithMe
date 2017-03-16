using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BrumWithMe.MVC.Hubs
{
    public class Notification : Hub
    {
        public void Log()
        {
            string a = "lognah";
            Clients.All.test(1);
        }
    }
}