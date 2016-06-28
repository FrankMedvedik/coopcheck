using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Ninject;
using Ninject.Web.Common;

namespace WEb.API2.Owin.NinJect
{
    public static class NinjectConfig
    {
        public static Lazy<IKernel> CreateKernel = new Lazy<IKernel>(() =>
        {
            var kernel = new StandardKernel();
            kernel.Load(Assembly.GetExecutingAssembly());

            RegisterServices(kernel);

            return kernel;
        });

        private static void RegisterServices(KernelBase kernel)
        {
            kernel.Bind<IFakeService>().To<FakeService>();
            kernel.Bind<IValuesProvider>().To<ValuesProvider>().InRequestScope();
        }
    }


    
}