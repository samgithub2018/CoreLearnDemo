using Autofac;
using Autofac.Extras.DynamicProxy;
using Core.Interface;
using Core.MainDemo.Filters;
using Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.MainDemo.Unity
{
    public class CustomAutofacRegisterModule : Module
    {
        /// <summary>
        /// autofac提供load的注册服务函数
        /// </summary>
        /// <param name="builder"></param>
        protected override void Load(ContainerBuilder builder)
        {
            //注册AOP
            builder.Register(s => new CustomAutofacAOP());
            builder.RegisterType<ClassA>().As<InterfaceA>().EnableInterfaceInterceptors();


            //注册服务
            //builder.RegisterType<Class1>().As<Interface1>();
            builder.RegisterType<Class2>().As<Interface2>();
            builder.RegisterType<Class3>().As<Interface3>();


        }

    }
}
