using StructureMap;

namespace UI.Universal.Extensions
{
    public static class IContainerExtensions
    {
        public static void RegisterConcreteTypeAsSingelton<T>(this IContainer container)
        {
            container.Configure(c => c.ForConcreteType<T>().Configure.Singleton());
        }
    }
}
