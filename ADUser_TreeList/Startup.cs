using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ADUser_TreeList.Startup))]
namespace ADUser_TreeList
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
