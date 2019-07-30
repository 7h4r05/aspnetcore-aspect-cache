using AspectCache.Demo.Cache;
using AspectCache.Demo.Models;
using AspectCache.Interfaces;
using AspectCache.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AspectCache
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMemoryCache();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            
            services.AddSingleton<ICache, InMemoryCache>();
            services.AddSingleton<ICacheKeyFactory<Item>, ItemKeyFactory>();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IHttpContextAccessor context)
        {
            app.UseMvc();
        }
    }
}
