using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Hangfire.Dashboard;
using Kasim.Core.BLL.InvoiceWebApp;
using Kasim.Core.IBLL.InvoiceWebApp;
using Kasim.Core.Model.InvoiceWebApp;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NLog.Extensions.Logging;
using NLog.Web;

namespace Kasim.Core.InvoiceWebApp
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
            services.Configure<ConnectionStringOptions>(Configuration.GetSection("ConnectionStrings"));

            services.AddHangfire(x =>
            {
                x.UseRedisStorage(Configuration.GetConnectionString("HangFireRedis"));                
            });
            services.AddTransient<IHangfireJobBLL, HangfireJobBLL>();

            services.AddAuthentication("MyCookieAuthenticationScheme")
            .AddCookie("MyCookieAuthenticationScheme", options => {
                options.AccessDeniedPath = "/Account/Forbidden/";
                options.LoginPath = "/Account/Unauthorized/";
            });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, IOptions<ConnectionStringOptions> connsOptions)
        {
            loggerFactory.AddNLog();
            env.ConfigureNLog("nlog.config");

            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseHangfireServer(new BackgroundJobServerOptions
            {
                Queues = new[] { "invoice", "default" },//队列名称，只能为小写 
                WorkerCount = Environment.ProcessorCount * 5, //并发任务数 
                ServerName = "hangfire_invoice",//服务器名称
            });          
            app.UseHangfireDashboard("/hangfire", new DashboardOptions
            {
                Authorization = new[] { new HangfireAuthorizationFilter() }
            });

            HangfireJobServer.JobStart(connsOptions.Value);

            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Invoice}/{action=Index}/{id?}");
            });
        }

        public class HangfireAuthorizationFilter : IDashboardAuthorizationFilter
        {
            //这里需要配置权限规则 
            public bool Authorize(DashboardContext context)
            {
                var httpContext = context.GetHttpContext();
                return httpContext.Session.GetString("login")=="52033";
            }
        }

        public static class HangfireJobServer
        {
            public static void JobStart(ConnectionStringOptions conns)
            {
                HangfireJobBLL.conns = conns;
                //RecurringJob.AddOrUpdate<IHangfireJobBLL>(x => x.DownloadInvoices(), Cron.Minutely(), queue: "invoice");
            }
        }
    }
}
