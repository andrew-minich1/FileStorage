using Ninject.Modules;
using System.Data.Entity;
using BLL_Interface;
using BLL_Interface.Services;
using BLL.Services;
using DAL.Concrete;
using DAL_Interface.Repository;
using ORM;
using Ninject.Web.Common;
using Ninject;
using System.Web.Configuration;


namespace DependencyResolver
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolver(this IKernel kernel)
        {
            kernel.Bind<DbContext>().To<FileStorageEntityModel>().InRequestScope();
            kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IRoleRepository>().To<RoleRepository>();
            kernel.Bind<IFileRepository>().To<FileRepository>();
            kernel.Bind<IUserService>().To<UserService>().WithConstructorArgument("path", WebConfigurationManager.AppSettings["Path"]);
            kernel.Bind<IFileService>().To<FileService>().WithConstructorArgument("path", WebConfigurationManager.AppSettings["Path"]);
        }
    }
}
