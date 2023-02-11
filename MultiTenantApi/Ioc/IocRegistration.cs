using MultiTenantApi.Interfaces;
using MultiTenantApi.Services;
using Unity;

namespace MultiTenantApi.Ioc
{
    public static class IocRegistration
    {
        public static IUnityContainer RegisterUnitMappings(IUnityContainer container)
        {
            container.RegisterType<IProductService, ProductService>();
            container.RegisterType<IUserService, UserService>();
            return container;
        }
    }
}