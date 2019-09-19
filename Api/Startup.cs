using Core.AppSettings;
using Core.Http.Request.Mail;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Service;
using Service.Helper;
using Service.Interfaces;

namespace Api
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
            services.Configure<ElasticSearchSettings>(Configuration.GetSection("ElasticSearch"));
            services.Configure<RabbitMQSettings>(Configuration.GetSection("RabbitMQ"));
            services.Configure<EmailSettings>(Configuration.GetSection("Email"));
            services.Configure<BusinesSettings>(Configuration.GetSection("Business"));
            
            services.AddScoped(typeof(IMailService<>),typeof(MailService<>));
            services.AddScoped(typeof(IElasticSearchService<>),typeof(ElasticSearchService<>));
            services.AddScoped(typeof(IRabbitMQService<>),typeof(RabbitMQService<>));
            services.AddSingleton(typeof(IMailHelper<MailRequest>),typeof(MailBodyHelper));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
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
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
