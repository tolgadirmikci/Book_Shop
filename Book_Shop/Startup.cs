using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Book_Shop.Startup))]
namespace Book_Shop
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
