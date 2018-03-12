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
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.Extensions.PlatformAbstractions;
using System.IO;
using Kasim.Core.WebApi.Common;

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

            //跨域
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

            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info
                {
                    Version = "v1", //版本号
                    Title = "Kasim.Core.WebApi接口文档", //标题
                    Description = "RESTful API ",
                    TermsOfService = "",//服务的条件
                                        //第一个参数Name 创建人名称/也可以是 负责人名称     第二个参数 联系邮箱
                    Contact = new Contact { Name = "KasimYe", Email = "lip8658@live.com", Url = "浙江慈溪" }
                });

                //获取设置配置信息的 的路径对象   swagger界面配置
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var xmlPath = Path.Combine(basePath, "Kasim.Core.WebApi.xml");
                x.IncludeXmlComments(xmlPath);
                x.OperationFilter<HttpHeaderOperation>(); // 添加httpHeader参数
            });
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
                       
            app.UseSwagger();
            // 指定站点
            app.UseSwaggerUI(x =>
            {
                //做出一个限制信息 描述
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "Kasim.Core API V1");
                //显示在发出请求时发送的标题
                //x.ShowRequestHeaders();
            });

            app.UseMvc();
        }
    }
}
