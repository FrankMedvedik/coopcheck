using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using DataClean.Contracts.Interfaces;
using DataClean.Contracts.Models;
using DataClean.DataCleaner;
using DataClean.Repository.Mgr;
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
            kernel.Bind<IDataCleanEventFactory>().To<DataCleanEventFactory>();
            kernel.Bind<IDataCleaner>().To<DataCleaner>();
            kernel.Bind<IDataCleanRepository>().To<DataCleanRespository>();
            kernel.Bind<IDataCleanCriteria>().To<DataCleanCriteria>();
        }
    }


    
}