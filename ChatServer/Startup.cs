using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Owin;
using Owin;
using System.Web.Http;

[assembly: OwinStartup(typeof(ChatServer.Startup))]

namespace ChatServer
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration c = new HttpConfiguration();
            WebApiConfig.Register(c);
            app.UseWebApi(c);
        }
    }
}
