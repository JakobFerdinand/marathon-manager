namespace Logic.DIConfiguration
{
    public interface IContainer
    {
        T GetInstance<T>();
        void RegisterConcreteTypeAsSingelton<T>();
    }
}
