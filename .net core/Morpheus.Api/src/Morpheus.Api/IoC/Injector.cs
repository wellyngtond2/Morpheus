using Autofac;
using MediatR;
using Microsoft.Extensions.Configuration;
using Morpheus.Core.Repositories;
using Morpheus.Data.Connector;
using Morpheus.Data.Repositories;
using Morpheus.Infrastructure.Infrastructure.Data;
using System;
using System.Linq;
using System.Reflection;

namespace Morpheus.Api.IoC
{
    public static class Injector
    {
        public static void RegisterIoc(ContainerBuilder builder, IConfiguration configuration)
        {

            var assemblies = new[]
            {
                Assembly.Load("Morpheus.Api"),
                typeof(IUnitOfWork).Assembly,
                typeof(UnitOfWork).Assembly,
            };

            builder
                .RegisterAssemblyTypes(assemblies)
                .Where(type => !type.IsAssignableToGenericType(typeof(INotificationHandler<>)))
                .AsImplementedInterfaces()
                .InstancePerLifetimeScope();

            string connectionString = configuration.GetConnectionString("dapperConnectionString");

            builder.Register(provider => new SqlServerConnector(connectionString))
                .As<IDbConnector>()
                .InstancePerLifetimeScope();
        }

        public static bool IsAssignableToGenericType(this Type givenType, Type genericType)
        {
            var interfaceTypes = givenType.GetInterfaces();

            if (interfaceTypes.Any(it => it.IsGenericType && it.GetGenericTypeDefinition() == genericType))
            {
                return true;
            }

            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
                return true;

            Type baseType = givenType.BaseType;
            if (baseType == null) return false;

            return IsAssignableToGenericType(baseType, genericType);
        }
    }
}