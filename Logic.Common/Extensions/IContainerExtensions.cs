using Microsoft.EntityFrameworkCore;
using StructureMap;
using System;

namespace Logic.Common.Extensions
{
    public static class IContainerExtensions
    {
        public static void RegisterConcreteTypeAsSingelton<T>(this IContainer container) => container.Configure(c => c.ForConcreteType<T>().Configure.Singleton());
        
        public static void AddDbContext<TContext>(this IContainer container, Action<DbContextOptionsBuilder<TContext>> config) where TContext : DbContext
        {
            var builder = new DbContextOptionsBuilder<TContext>();
            config(builder);

            container.Configure(c => c.ForConcreteType<TContext>().Configure
                .Ctor<DbContextOptions<TContext>>()
                .Is(builder.Options));
        }
    }
}
