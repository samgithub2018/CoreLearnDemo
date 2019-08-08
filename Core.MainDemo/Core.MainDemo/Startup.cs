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
using Core.MainDemo.Middlewares;

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
                //【123】 Filters 扩展注册，Filters主要是针对action 进行业务扩展的，
                //但CustomResourceFilterAttribute这个和权限filter却是在实例化控制前执行的
                //_.Filters.Add<CustomExceptionFilterAttribute>(); //不知道为什么这个会失败
                //_.Filters.Add<CustomResultFilterAttribute>();
                //_.Filters.Add<CustomResourceFilterAttribute>();
                //_.Filters.Add<CustomIActionFilterAttribute>();
                //测试别的 先注释

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


            #region 中间件，这个是针对控制器之外的业务扩展的【115】


            //“终结点”中间件，没有Next.Invoke() 下一步，一般作为终结点中间件
            //app.Run(context =>
            //{
            //    return context.Response.WriteAsync("Hello World First");
            //});

            //将委托添加到请求的管道里面（中间件）【1】
            //app.Use(next =>
            //{
            //    return new RequestDelegate(async context =>
            //    {
            //        //await context.Response.WriteAsync("Hello World this 1!");
            //        await next.Invoke(context);
            //        //await context.Response.WriteAsync("Hello World this 2!");
            //    });
            //});

            ////"内嵌定义" 委托中间件，这个Use是对上面的扩展，做了一个扩展方法 【2】
            //app.Use(async (context, next) =>
            //{
            //    //这个Next.Invoke()代表执行下个中间件，知道执行完所有中间件，
            //    //然后实例化Controller执行Action，没有这个的话，请求到这里就会结束了
            //    //await context.Response.WriteAsync("Hello World this 3!");
            //    await next.Invoke();
            //    //await context.Response.WriteAsync("Hello World this 4!");
            //});


            ////判断请求路径，如果通过则执行分支中间件
            //app.Map("/TestAction", appBuilder =>
            //{
            //    appBuilder.Run(async context =>
            //    {
            //        await context.Response.WriteAsync("this map");
            //    });
            //});

            ////根据httpContext上下文内容做出判断，如果通过判断则执行分支中间内容
            //app.MapWhen(context =>
            //{
            //    return context.Request.Query.Keys.Contains("Name");
            //},
            //appBuilder =>
            //{
            //    appBuilder.Run(async context =>
            //    {
            //        await context.Response.WriteAsync("this MapWhen");
            //    });
            //});

            //通过对象类注册中间件,将本来复杂的业务分离
            app.UseMiddleware<LogMiddleware>();
            app.UseMiddleware<ValidateMiddleware>();


            #endregion


            //【366】注册服务 中间件
            app.UseHttpsRedirection();//注册https，这个是默认帮注册的，不能删，只能改
            app.UseStaticFiles();//注册seesion
            app.UseCookiePolicy();//注册cookie
            app.UseMvc(routes =>//这个也是一个中间件
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
