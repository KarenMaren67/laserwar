using Application;
using AutoMapper;
using DAL.EF;
using DryIoc;
using Prism.DryIoc;
using Prism.Ioc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Departments.Extensions
{
    public static class IContainerRegistryExtensions
    {
        public static IContainerRegistry AddAutoMapper(this IContainerRegistry services)
        {
            return services.AddAutoMapper(
                typeof(DepartmentModule),
                typeof(ApplicationModule),
                typeof(DALModule)
                );
        }

        private static IContainerRegistry AddAutoMapper(this IContainerRegistry services, params Type[] profileAssemblyMarkerTypes)
        {
            return AddAutoMapperClasses(services, null, profileAssemblyMarkerTypes.Select(t => t.GetTypeInfo().Assembly));
        }

        private static IContainerRegistry AddAutoMapperClasses(IContainerRegistry services,
                                                               Action<IServiceProvider, IMapperConfigurationExpression> configAction,
                                                               IEnumerable<Assembly> assembliesToScan)
        {
            // Just return if we've already added AutoMapper to avoid double-registration
            if (services.IsRegistered(typeof(IMapper)))
            {
                return services;
            }

            assembliesToScan = assembliesToScan as Assembly[] ?? assembliesToScan.ToArray();

            var allTypes = assembliesToScan
                .Where(a => !a.IsDynamic && a.GetName().Name != nameof(AutoMapper))
                .Distinct() // avoid AutoMapper.DuplicateTypeMapConfigurationException
                .SelectMany(a => a.DefinedTypes)
                .ToArray();

            var openTypes = new[]
            {
                typeof(IValueResolver<,,>),
                typeof(IMemberValueResolver<,,,>),
                typeof(ITypeConverter<,>),
                typeof(IValueConverter<,>),
                typeof(IMappingAction<,>)
            };
            foreach (var type in openTypes.SelectMany(openType => allTypes
                .Where(t => t.IsClass
                    && !t.IsAbstract
                    && t.AsType().ImplementsGenericInterface(openType))))
            {
                services.Register(type.AsType());
            }
            var container = services.GetContainer();
            container.RegisterDelegate<IConfigurationProvider>(x => new MapperConfiguration(cfg => ConfigAction(x, cfg)), Reuse.Singleton);

            container.RegisterDelegate<IMapper>(x => new Mapper(x.Resolve<IConfigurationProvider>()), Reuse.Singleton);

            return services;

            void ConfigAction(IServiceProvider serviceProvider, IMapperConfigurationExpression cfg)
            {
                configAction?.Invoke(serviceProvider, cfg);

                cfg.AddMaps(assembliesToScan);
            }
        }

        private static bool ImplementsGenericInterface(this Type type, Type interfaceType)
            => type.IsGenericType(interfaceType) || type.GetTypeInfo().ImplementedInterfaces.Any(@interface => @interface.IsGenericType(interfaceType));

        private static bool IsGenericType(this Type type, Type genericType)
            => type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == genericType;
    }
}
