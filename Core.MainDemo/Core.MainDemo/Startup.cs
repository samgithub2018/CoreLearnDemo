using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Autofac;
using Core.MainDemo.Unity;
using Autofac.Extensions.DependencyInjection;
using System;
using Core.MainDemo.Filters;
using Autofac.Configuration;
using Core.Interface;

namespace Core.MainDemo
{
    /// <summary>
    /// c
    /// 
    /// 
    /// 
    /// 
    /// </summary>
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddSession();
            services.AddMvc(_ =>
            {
                //【123】 Filters 扩展注册
                //_.Filters.Add<CustomExceptionFilterAttribute>(); //不知道为什么这个会失败
                _.Filters.Add<CustomResultFilterAttribute>();
                _.Filters.Add<CustomResourceFilterAttribute>();
                _.Filters.Add<CustomIActionFilterAttribute>();
                //_.Filters.Add<CustomActionFilterAttribute>();

            }).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            #region autofac容器扩展【333】

            //1.实例一个容器
            Autofac.ContainerBuilder containerBuilder = new ContainerBuilder();

            //2.通过配置文件注册服务
            IConfigurationBuilder configuration = new ConfigurationBuilder();
            configuration.AddJsonFile("Config/autofac.json");
            IConfigurationRoot rootBuil = configuration.Build();
            var module = new ConfigurationModule(rootBuil);
            containerBuilder.RegisterModule(module);

            //2.通过硬代码注册服务
            containerBuilder.RegisterModule<CustomAutofacRegisterModule>();
            //builder.RegisterType<Class1>().As<Interface1>();也可以这样注册，但是放到一个类统一注册比较规范

            //3.容器替换
            containerBuilder.Populate(services);

            //4.将扩展的 autofac容器 返回给框架
            IContainer container = containerBuilder.Build();
            IServiceProvider serviceProvider = new AutofacServiceProvider(container);
            return serviceProvider;

            #endregion

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
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

            //【356】读取appsettings.json配置文件
            string detaulLoglevel = this.Configuration["Logging:LogLevel:Default"];//这个不知道为什么取不到
            string testStr = this.Configuration["de:ddd:aaa"];//自定义的没问题


            //log4net日志对象
            ILogger<Startup> logger = loggerFactory.CreateLogger<Startup>();
            logger.LogError("thie Startup inir error");


            //终结点，没有下一步
            app.Run(context =>
            {
                return context.Response.WriteAsync("Hello World");
            });


            //终结点，没有下一步
            app.Use(async (context, next) =>
            {
                //await next.Invoke(); 没有这个的话，请求到这里就会结束了，所谓的请求终结点
                await context.Response.WriteAsync("Hello World!");
            });

            //【366】注册服务
            app.UseHttpsRedirection();//注册https，这个是默认帮注册的，不能删，只能改
            app.UseStaticFiles();//注册seesion
            app.UseCookiePolicy();//注册cookie
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
