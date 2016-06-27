//using CoopCheck.Domain.Contracts.Interfaces;
//using CoopCheck.Domain.Services;
//using CoopCheck.Library;
//using CoopCheck.Repository;
//using DataClean.Contracts.Interfaces;
//using DataClean.Contracts.Models;
//using DataClean.DataCleaner;
//using DataClean.Repository.Mgr;
//using Ninject;
//using Ninject.Modules;

//namespace CoopCheck.Web.IOC
//{
//    public class RunTimeModule : NinjectModule
//    {
//        public override void Load()
//        {
//            Bind<ISendMailSvc>().To<SendMailSvc>();
//            Bind<ISwiftPaySvc>().To<SwiftPaySvc>();
//            Bind<IUserAuthSvc>().To<UserAuthSvc>();
//            Bind<ICoopCheckEntities>().To<CoopCheckEntities>();
//            Bind<IDataCleanEventFactory>().To<DataCleanEventFactory>();
//            Bind <IDataCleaner>().To <DataCleaner>();
//            Bind <IDataCleanRepository>().To<DataCleanRespository>();
//            Bind <IDataCleanCriteria>().To<DataCleanCriteria>();

//        }
//    }
//    public static class IocKernel
//    {
//        private static StandardKernel _kernel;

//        public static T Get<T>()
//        {
//            return _kernel.Get<T>();
//        }

//        public static void Initialize(params INinjectModule[] modules)
//        {
//            if (_kernel == null)
//            {
//                _kernel = new StandardKernel(modules);
//            }
//        }
//    }
//}
