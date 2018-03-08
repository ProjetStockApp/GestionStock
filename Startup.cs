using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(StockApp.Startup))]
namespace StockApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
