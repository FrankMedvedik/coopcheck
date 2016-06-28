//using CoopCheck.Domain.Contracts.Interfaces;
//using CoopCheck.Domain.Services;
//using CoopCheck.Repository;
//using DataClean.Contracts.Interfaces;
//using DataClean.Contracts.Models;
//using DataClean.DataCleaner;
//using DataClean.Repository.Mgr;

//[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(CoopCheck.Web.App_Start.NinjectWebCommon), "Start")]
//[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(CoopCheck.Web.App_Start.NinjectWebCommon), "Stop")]

//namespace CoopCheck.Web.App_Start
//{
//    using System;
//    using System.Web;

//    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

//    using Ninject;
//    using Ninject.Web.Common;

//    public static class NinjectWebCommon
//    {
//        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

//        /// <summary>
//        /// Starts the application
//        /// </summary>
//        public static void Start()
//        {
//            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
//            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
//            bootstrapper.Initialize(CreateKernel);
//        }

//        /// <summary>
//        /// Stops the application.
//        /// </summary>
//        public static void Stop()
//        {
//            bootstrapper.ShutDown();
//        }

//        /// <summary>
//        /// Creates the kernel that will manage your application.
//        /// </summary>
//        /// <returns>The created kernel.</returns>
//        private static IKernel CreateKernel()
//        {
//            var kernel = new StandardKernel();
//            try
//            {
//                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
//                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

//                RegisterServices(kernel);
//                return kernel;
//            }
//            catch
//            {
//                kernel.Dispose();
//                throw;
//            }
//        }

//        /// <summary>
//        /// Load your modules or register your services here!
//        /// </summary>
//        /// <param name="kernel">The kernel.</param>
//        private static void RegisterServices(IKernel kernel)
//        {

//            kernel.Bind<ISendMailSvc>().To<SendMailSvc>();
//            kernel.Bind<ISwiftPaySvc>().To<SwiftPaySvc>();
//            kernel.Bind<IUserAuthSvc>().To<UserAuthSvc>();
//            kernel.Bind<ICoopCheckEntities>().To<CoopCheckEntities>();
//            kernel.Bind<IDataCleanEventFactory>().To<DataCleanEventFactory>();
//            kernel.Bind<IDataCleaner>().To<DataCleaner>();
//            kernel.Bind<IDataCleanRepository>().To<DataCleanRespository>();
//            kernel.Bind<IDataCleanCriteria>().To<DataCleanCriteria>();
//        }
//    }
//}
