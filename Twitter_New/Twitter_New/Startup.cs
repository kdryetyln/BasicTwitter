using Microsoft.Owin;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

[assembly: OwinStartup(typeof(Twitter_New.Startup))]

namespace Twitter_New
{
    public class Startup
    {
        public void Configuration(IAppBuilder App)
        {

            App.MapSignalR();
        }
    }
}