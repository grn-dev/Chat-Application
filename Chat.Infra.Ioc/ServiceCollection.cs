using Chat.Domain.Attributes;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Chat.Infra.Ioc
{
    public class ServiceCollection
    {


        private IServiceCollection _serviceDescriptors;



        public ServiceCollection(IServiceCollection serviceDescriptors)
        {
            _serviceDescriptors = serviceDescriptors;
        }


        public void AddServiceDescriptors()
        {
            //var loadedAssemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();
            //var loadedPaths = loadedAssemblies.Select(a => a.Location).ToArray();

            //var referencedPaths = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory, "*.dll");
            //var toLoad = referencedPaths.Where(r => !loadedPaths.Contains(r, StringComparer.InvariantCultureIgnoreCase)).ToList();

            //toLoad.ForEach(path => loadedAssemblies.Add(AppDomain.CurrentDomain.Load(AssemblyName.GetAssemblyName(path))));

            //var projects = AppDomain.CurrentDomain.GetAssemblies()
            //    .Select(assembly => assembly.GetName().Name)
            //    .ToList();

            var projects = new List<string>()
             {"Chat.Infra", "Chat.Infra.Bus", "Chat.Application",
              "Chat.Application.Service", "Chat.Domain", "Chat.Domain.Core", "Chat.Infra.Data", "Chat.Infra.Security","Chat" };
            var typesToRegister = new List<Type>();
            foreach (var item in projects)
            {
                typesToRegister.AddRange(GetTypesByAssemblyName(item));
            }


            foreach (var item in typesToRegister)
            {
                var service = GetServiceBehavior(GetServiceLifetime(item));
                var InterfacesTypes = item.GetInterfaces().ToList();
                if (InterfacesTypes.Count == 0)
                    service.Add(item);
                else
                    InterfacesTypes.ForEach(x => service.Add(GetServiceType(x, item), item));
            }
        }

        private List<Type> GetTypesByAssemblyName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return new List<Type>();

            return Assembly.Load(name)
               .GetTypes()
               .Where(x => x.GetCustomAttributes(typeof(BeanAttribute), false).Length > 0)
               .ToList();
        }
        private Domain.Enums.ServiceLifetime GetServiceLifetime(Type type)
        {
            if (type.GetCustomAttributes(typeof(ScopeAttribute), false).Length > 0)
                return ((ScopeAttribute)type.GetCustomAttributes(typeof(ScopeAttribute), false)[0]).GetServiceLifetime();
            else
                return Domain.Enums.ServiceLifetime.Scoped;
        }

        private Type GetServiceType(Type serviceType, Type implementationType)
        {
            return implementationType.IsGenericType && !serviceType.IsGenericTypeDefinition
                ? serviceType.GetGenericTypeDefinition()
                : serviceType;
        }

        private IServiceBehavior GetServiceBehavior(Domain.Enums.ServiceLifetime serviceLifetime)
        {

            var type = Assembly.Load("Chat.Infra.Ioc")
               .GetTypes()
                .FirstOrDefault(x =>
                 x.IsClass &&
                  x.GetInterfaces().Any(i => i.Name == "IServiceBehavior") &&
                  x.GetCustomAttribute<ScopeAttribute>().GetServiceLifetime() == serviceLifetime
                );
            return (IServiceBehavior)Activator.CreateInstance(type, new Object[] { _serviceDescriptors });
        }

    }
}
