namespace Core.EventAggregation
{
    public interface IEventAggregator
    {
        TEvent GetEvent<TEvent>() where TEvent : EventBase, new();
    }
}
