using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Chat.Infra.Data.Configurations.BaseModelConfigurations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Chat.Infra.Data.Configurations.Applicator
{
    public static class ConfigurationApplicator
    {
        /// <summary>
        /// This extension create an instance of any class that inherites from <see cref="BaseModelConfiguration{TEntity, TPrimaryKey}"/>
        /// with an EntityTypeBuilder of <see cref="BaseModelConfiguration{TEntity, TPrimaryKey}"/> generic type
        /// </summary>
        /// <param name="modelBuilder"></param>
        public static void ApplyAllConfigurations(this ModelBuilder modelBuilder)
        {
            var configurations = GetConfigurationWrappers();

            foreach (var config in configurations)
            {
                var entityTypeBuilder = GetEntityTypeBuilder(modelBuilder, config.Model);

                Activator.CreateInstance(config.Config, entityTypeBuilder);
            }
        }

        private static IList<ConfigurationWrapper> GetConfigurationWrappers()
        {
            List<ConfigurationWrapper> configurationWrappers = new List<ConfigurationWrapper>();

            var configTypes = GetAllConfigTypes();

            foreach (Type type in configTypes)
                configurationWrappers.Add(BuildConfiguratonWrapper(type));


            return configurationWrappers;
        }

        private static IEnumerable<Type> GetAllConfigTypes()
        {
            var types = Assembly
                .GetAssembly(typeof(BaseModelConfiguration<,>))
                .GetTypes()
                .Where(type =>
                    type.IsClass &&
                    !type.IsAbstract &&
                    !type.IsGenericType &&
                    InheritsOrImplements(type, typeof(BaseModelConfiguration<,>)))
                .ToList();

            return types;
        }

        private static ConfigurationWrapper BuildConfiguratonWrapper(Type configurationType)
        {
            Type model = configurationType.BaseType.GetGenericArguments()[0];

            var config = new ConfigurationWrapper()
            {
                Model = model,
                Config = configurationType
            };

            return config;
        }

        private static EntityTypeBuilder GetEntityTypeBuilder(ModelBuilder modelBuilder, Type modelType)
        {
            var entityMethod = typeof(ModelBuilder)
                .GetMethods()
                .Where(m =>
                    m.Name == "Entity" &&
                    m.IsGenericMethod &&
                    m.GetParameters().Count() == 0)
                .FirstOrDefault();

            if (entityMethod == null)
                throw new InvalidOperationException("Method Entity not found");


            entityMethod = entityMethod.MakeGenericMethod(modelType);

            var entityTypeBuilder = (EntityTypeBuilder)(entityMethod.Invoke(modelBuilder, null));

            return entityTypeBuilder;
        }

        #region InheritsOrImplements

        private static bool InheritsOrImplements(this Type child, Type parent)
        {
            parent = ResolveGenericTypeDefinition(parent);

            var currentChild = child.IsGenericType
                                   ? child.GetGenericTypeDefinition()
                                   : child;

            while (currentChild != typeof(object))
            {
                if (parent == currentChild || HasAnyInterfaces(parent, currentChild))
                    return true;

                currentChild = currentChild.BaseType != null
                               && currentChild.BaseType.IsGenericType
                                   ? currentChild.BaseType.GetGenericTypeDefinition()
                                   : currentChild.BaseType;

                if (currentChild == null)
                    return false;
            }
            return false;
        }

        private static bool HasAnyInterfaces(Type parent, Type child)
        {
            return child.GetInterfaces()
                .Any(childInterface =>
                {
                    var currentInterface = childInterface.IsGenericType
                        ? childInterface.GetGenericTypeDefinition()
                        : childInterface;

                    return currentInterface == parent;
                });
        }

        private static Type ResolveGenericTypeDefinition(Type parent)
        {
            var shouldUseGenericType = true;
            if (parent.IsGenericType && parent.GetGenericTypeDefinition() != parent)
                shouldUseGenericType = false;

            if (parent.IsGenericType && shouldUseGenericType)
                parent = parent.GetGenericTypeDefinition();

            return parent;
        }

        #endregion
    }
}
