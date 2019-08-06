using Autofac.Extras.DynamicProxy;
using Castle.DynamicProxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.MainDemo.Unity
{
    public class CustomAutofacAOP : IInterceptor
    {
        public void Intercept(IInvocation invocation)
        {
            this.Front();
            invocation.Proceed();
            this.Back();
        }


        public void Front()
        {
            Console.WriteLine("方法执行前！");
        }

        public void Back()
        {
            Console.WriteLine("方法执行后！");
        }
    }


    public interface InterfaceA
    {
        void Show();
    }

    [Intercept(typeof(CustomAutofacAOP))]
    public class ClassA : InterfaceA
    {
        public void Show()
        {
            Console.WriteLine("this ClassA");
        }
    }




}
