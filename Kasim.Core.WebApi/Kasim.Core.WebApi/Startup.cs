using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Kasim.Core.Model.WebApi;
using NLog.Extensions.Logging;
using NLog.Web;

namespace Kasim.Core.WebApi
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
            services.AddOptions();
            services.Configure<ConnectionStringOptions>(Configuration.GetSection("ConnectionStrings"));
            services.Configure<ApiOptions>(app =>
            {
                app.AppImgSavePath = new ImgSavePath { Path = Configuration.GetSection("AppSetting:ImgSavePath")["Path"]};
                app.AppImgServer = new ImgServer {
                    CostImg = Configuration.GetSection("AppSetting:ImgServer")["CostImg"],
                    Tax= Configuration.GetSection("AppSetting:ImgServer")["Tax"]
                };
                app.AppImgThumbnail = new ImgThumbnail {
                    Width= int.Parse(Configuration.GetSection("AppSetting:ImgThumbnail")["Width"]),
                    Height = int.Parse(Configuration.GetSection("AppSetting:ImgThumbnail")["Height"]),
                    Model = Configuration.GetSection("AppSetting:ImgThumbnail")["Model"],
                };
            });
            services.AddCors(options =>
            {
                options.AddPolicy("any", builder =>
                {
                    builder.AllowAnyOrigin() //允许任何来源的主机访问
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials();//指定处理cookie
                });
            });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddNLog();
            env.ConfigureNLog("nlog.config");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
