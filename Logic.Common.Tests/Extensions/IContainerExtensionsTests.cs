using Logic.Common.Extensions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using StructureMap;
using System;
using Xunit;

namespace Logic.Common.Tests.Extensions
{
    public class IContainerExtensionsTests
    {
        [Fact]
        public void RegisterConcreteTypeAsSingelton_registers_a_Type_as_Singelton()
        {
            var container = Substitute.For<IContainer>();

            container.RegisterConcreteTypeAsSingelton<TheTypeToRegister>();

            container.WhenForAnyArgs(c => c.Configure(null)).Do(i => i.Arg<ConfigurationExpression>().Received().ForConcreteType<TheTypeToRegister>());
            container.ReceivedWithAnyArgs().Configure(null);
        }
        [Fact]
        public void AddDbContext_registers_the_Context_with_options()
        {
            var container = Substitute.For<IContainer>();

            DbContextOptionsBuilder<TheContextToRegister> builder = null;
            Action<DbContextOptionsBuilder<TheContextToRegister>> options = b => builder = b;

            container.AddDbContext(options);

            Assert.NotNull(builder);
            container.ReceivedWithAnyArgs().Configure(null);
            container.WhenForAnyArgs(c => c.Configure(null)).Do(i => i.Arg<ConfigurationExpression>().Received().ForConcreteType<TheContextToRegister>());
            container.WhenForAnyArgs(c => c.Configure(null)).Do(i => i.Arg<ConfigurationExpression>().ForConcreteType<TheContextToRegister>().Configure.Received().Ctor<DbContextOptions<TheContextToRegister>>());
            container.WhenForAnyArgs(c => c.Configure(null)).Do(i => i.Arg<ConfigurationExpression>().ForConcreteType<TheContextToRegister>().Configure.Ctor<DbContextOptions<TheContextToRegister>>().Received().Is(builder.Options));
        }

        class TheTypeToRegister { }
        class TheContextToRegister : DbContext { }
    }
}
