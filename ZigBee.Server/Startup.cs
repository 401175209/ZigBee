using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(ZigBee.Server.Startup))]
namespace ZigBee.Server
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // Any connection or hub wire up and configuration should go here
            app.MapSignalR();
        }
    }
}