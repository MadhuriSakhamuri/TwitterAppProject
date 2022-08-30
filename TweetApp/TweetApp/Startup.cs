using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Threading.Tasks;
using TweetApp.Models;
using TweetApp.Services;

namespace TweetApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddControllersWithViews();
            services.Configure<TweetDataDatabaseSettings>(Configuration.GetSection("TweetDataDatabase"));
            services.AddSingleton<UserDataService>();
            services.AddControllers()
                    .AddJsonOptions(
                    options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
            services.AddSwaggerGen(c=> { c.EnableAnnotations(); });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapControllerRoute(
                //    name: "DefaultApiWithAction",
                //    //pattern: "{controller=Home}/{action=Index}/{id?}");
                //    pattern: "{controller}/{action}/{id?}",
                //    defaults: new { id = RouteParameter.Optional ,action= "GetUsers" });

       //         endpoints.MapHttpRoute(
       //name: "swagger_root",
       //routeTemplate: "",
       //defaults: null,
       //constraints: null,
       //handler: new RedirectHandler((message => message.RequestUri.ToString()), "swagger"));

                endpoints.MapControllerRoute(name: "default",
               pattern: "{controller=Tweet}/{action}/{id?}");
                endpoints.MapControllerRoute(name: "UserData",
                pattern: "UserData/{*GetTweet}",
                defaults: new { controller = "UserData", action = "GetTweet" });
            });
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Twitter API"); });
            app.Run(context => {
                context.Response.Redirect("swagger/index.html");
                return Task.CompletedTask;
            });
        }
    }
}
