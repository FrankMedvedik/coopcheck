using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using CoopCheck.Domain.Contracts.Interfaces;
using CoopCheck.Domain.Services;
using CoopCheck.Repository;
using DataClean.Contracts.Interfaces;
using DataClean.Contracts.Models;
using DataClean.DataCleaner;
using DataClean.Repository.Mgr;
using Ninject;

namespace CoopCheck.Web
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
            //kernel.Bind<IFakeService>().To<FakeService>();
            //kernel.Bind<IValuesService>().To<FakeService>();
            kernel.Bind<ISendMailSvc>().To<SendMailSvc>();
            kernel.Bind<ISwiftPaySvc>().To<SwiftPaySvc>();
            kernel.Bind<IUserAuthSvc>().To<UserAuthSvc>();
            kernel.Bind<ICoopCheckEntities>().To<CoopCheckEntities>();
            kernel.Bind<IDataCleanEventFactory>().To<DataCleanEventFactory>();
            kernel.Bind<IDataCleaner>().To<DataCleaner>();
            kernel.Bind<IDataCleanRepository>().To<DataCleanRespository>();
            kernel.Bind<IDataCleanCriteria>().To<DataCleanCriteria>();
        }
    }
}