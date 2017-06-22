using System;

namespace Logic.DIConfiguration
{
    public class Container : IContainer
    {
        private readonly StructureMap.Container _container;

        public Container()
        {
            _container = new StructureMap.Container(c =>
            {
                c.AddRegistry(new CommonRegistry());
                c.AddRegistry(new LoggingRegistry());
                c.AddRegistry(new DataRegistry());
            });
        }

        public T GetInstance<T>()
        {
            return _container.GetInstance<T>();
        }

        public void RegisterConcreteTypeAsSingelton<T>()
        {
            _container.Configure(c => c.ForConcreteType<T>().Configure.Singleton());
        }
    }
}
