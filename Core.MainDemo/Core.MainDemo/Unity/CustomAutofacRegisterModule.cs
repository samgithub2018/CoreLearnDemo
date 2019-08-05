using Autofac;
using Core.Interface;
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
            builder.RegisterType<Class1>().As<Interface1>();
            builder.RegisterType<Class2>().As<Interface2>();
            builder.RegisterType<Class3>().As<Interface3>();
        }

    }
}
