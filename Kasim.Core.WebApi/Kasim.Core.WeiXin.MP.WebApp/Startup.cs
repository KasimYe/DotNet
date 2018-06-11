using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hangfire;
using Kasim.Core.BLL.WeiXin.MP.WebApp;
using Kasim.Core.Factory.Weixin;
using Kasim.Core.Model.Weixin;
using Kasim.Core.WeiXin.MP.CommonService.CustomMsg;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Senparc.Weixin.Cache;
using Senparc.Weixin.Cache.Memcached;
using Senparc.Weixin.Cache.Redis;
using Senparc.Weixin.Entities;
using Senparc.Weixin.MP.Containers;
using Senparc.Weixin.Threads;

namespace Kasim.Core.WeiXin.MP.WebApp
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
            services.AddMvc();
            //添加Senparc.Weixin配置文件（内容可以根据需要对应修改）
            services.Configure<SenparcWeixinSetting>(Configuration.GetSection("SenparcWeixinSetting"));

            services.AddHangfire(x => x.UseSqlServerStorage(Configuration.GetConnectionString("DefaultConnection")));

            services.Configure<HangfireJobCron>(Configuration.GetSection("HangfireJobCron"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
            IOptions<SenparcWeixinSetting> senparcWeixinSetting, 
            IOptions<HangfireJobCron> hangfireJobCron, 
            IOptions<ConnectionStringOptions> connectionStringOptions)
        {
            //引入EnableRequestRewind中间件
            app.UseEnableRequestRewind();

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

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            //启动Hangfire
            app.UseHangfireServer();
            app.UseHangfireDashboard();

            #region 微信相关

            ////注册微信
            //AccessTokenContainer.Register(senparcWeixinSetting.Value.WeixinAppId, senparcWeixinSetting.Value.WeixinAppSecret);

            //Senparc.Weixin SDK 配置
            Senparc.Weixin.Config.IsDebug = true;
            Senparc.Weixin.Config.DefaultSenparcWeixinSetting = senparcWeixinSetting.Value;

            //提供网站根目录
            if (env.ContentRootPath != null)
            {
                Senparc.Weixin.Config.RootDictionaryPath = env.ContentRootPath;
                CommonService.Utilities.Server.AppDomainAppPath = env.ContentRootPath;// env.ContentRootPath;
            }
            CommonService.Utilities.Server.WebRootPath = env.WebRootPath;// env.ContentRootPath;



            /* 微信配置开始
             * 
             * 建议按照以下顺序进行注册，尤其须将缓存放在第一位！
             */

            RegisterWeixinCache();      //注册分布式缓存（按需，如果需要，必须放在第一个）
            ConfigWeixinTraceLog();     //配置微信跟踪日志（按需）
            RegisterWeixinThreads();    //激活微信缓存及队列线程（必须）
            RegisterSenparcWeixin();    //注册Demo所用微信公众号的账号信息（按需）

            /* 微信配置结束 */

            #endregion

            StartQuartzJob(senparcWeixinSetting, hangfireJobCron, connectionStringOptions);
        }

        /// <summary>
        /// 自定义缓存策略
        /// </summary>
        private void RegisterWeixinCache()
        {
            var senparcWeixinSetting = Senparc.Weixin.Config.DefaultSenparcWeixinSetting;

            //如果留空，默认为localhost（默认端口）

            #region  Redis配置
            var redisConfiguration = senparcWeixinSetting.Cache_Redis_Configuration;
            RedisManager.ConfigurationOption = redisConfiguration;

            //如果不执行下面的注册过程，则默认使用本地缓存

            if (!string.IsNullOrEmpty(redisConfiguration) && redisConfiguration != "Redis配置")
            {
                CacheStrategyFactory.RegisterObjectCacheStrategy(() => RedisObjectCacheStrategy.Instance);//Redis
            }

            #endregion

            #region Memcached 配置

            var memcachedConfig = new Dictionary<string, int>()
            {
                { "localhost",9101 }
            };
            MemcachedObjectCacheStrategy.RegisterServerList(memcachedConfig);

            #endregion

            //CacheStrategyFactory.RegisterContainerCacheStrategy(() => MemcachedContainerStrategy.Instance);//Memcached
        }

        /// 配置微信跟踪日志
        /// </summary>
        private void ConfigWeixinTraceLog()
        {
            //这里设为Debug状态时，/App_Data/WeixinTraceLog/目录下会生成日志文件记录所有的API请求日志，正式发布版本建议关闭
            Senparc.Weixin.Config.IsDebug = true;
            Senparc.Weixin.WeixinTrace.SendCustomLog("系统日志", "系统启动");//只在Senparc.Weixin.Config.IsDebug = true的情况下生效

            //自定义日志记录回调
            Senparc.Weixin.WeixinTrace.OnLogFunc = () =>
            {
                //加入每次触发Log后需要执行的代码
            };

            //当发生基于WeixinException的异常时触发
            Senparc.Weixin.WeixinTrace.OnWeixinExceptionFunc = ex =>
            {
                //加入每次触发WeixinExceptionLog后需要执行的代码

                //发送模板消息给管理员
                var eventService = new CommonService.EventService();
                eventService.ConfigOnWeixinExceptionFunc(ex);
            };
        }

        /// <summary>
        /// 激活微信缓存
        /// </summary>
        private void RegisterWeixinThreads()
        {
            ThreadUtility.Register();//如果不注册此线程，则AccessToken、JsTicket等都无法使用SDK自动储存和管理。
        }

        /// <summary>
        /// 注册Demo所用微信公众号的账号信息
        /// </summary>
        private void RegisterSenparcWeixin()
        {
            var senparcWeixinSetting = Senparc.Weixin.Config.DefaultSenparcWeixinSetting;

            //注册公众号
            AccessTokenContainer.Register(
                senparcWeixinSetting.WeixinAppId,
                senparcWeixinSetting.WeixinAppSecret,
                "【麦斯康莱】公众号");

            //注册小程序（完美兼容）
            AccessTokenContainer.Register(
                senparcWeixinSetting.WxOpenAppId,
                senparcWeixinSetting.WxOpenAppSecret,
                "【麦斯康莱】小程序");
        }


        private void StartQuartzJob(IOptions<SenparcWeixinSetting> senparcWeixinSetting, IOptions<HangfireJobCron> hangfireJobCron, IOptions<ConnectionStringOptions> connectionStringOptions)
        {
            //var context = new CommonService.ManageHandlers.ProductHandler.QHProductContext(
            //        Configuration.GetConnectionString("DevConnection"),
            //        Configuration.GetConnectionString("ActionConnection"),
            //        senparcWeixinSetting.Value.WeixinAppId);

            ModelFactory.SenparcWeixinSetting = senparcWeixinSetting.Value;
            ModelFactory.ConnectionStringOptions = connectionStringOptions.Value;
            ////缺货任务
            //RecurringJob.AddOrUpdate(
            //    () => SendNewsContext.SendNewsQHProducts(), hangfireJobCron.Value.QHProduct,
            //    TimeZoneInfo.Local);

            ////集单任务
            //RecurringJob.AddOrUpdate(() => SendNewsContext.SendNewsHYOrder(), hangfireJobCron.Value.HYOrder,
            //        TimeZoneInfo.Local);

            //发票任务
            RecurringJob.AddOrUpdate(() => SendNewsContext.SendNewsTaxBills(), hangfireJobCron.Value.TaxBill,
                    TimeZoneInfo.Local);

            ////出库任务
            //RecurringJob.AddOrUpdate(() => SendNewsContext.SendNewsSaleBills(), hangfireJobCron.Value.SaleBill,
            //        TimeZoneInfo.Local);

            ////任务每分钟执行一次
            //RecurringJob.AddOrUpdate(() => Console.WriteLine($"ASP.NET Core LineZero"), Cron.Minutely());
            ////任务执行一次
            //BackgroundJob.Enqueue(() => Console.WriteLine($"ASP.NET Core One Start LineZero{DateTime.Now}"));
            ////任务延时两分钟执行
            //BackgroundJob.Schedule(() => Console.WriteLine($"ASP.NET Core await LineZero{DateTime.Now}"), TimeSpan.FromMinutes(2));


        }
    }
}
