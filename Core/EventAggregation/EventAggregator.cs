using System;
using System.Collections.Concurrent;

namespace Core.EventAggregation
{
    public sealed class EventAggregator : IEventAggregator
    {
        private readonly object _lockObject = new object();
        private readonly ConcurrentDictionary<Type, EventBase> _events = new ConcurrentDictionary<Type, EventBase>();

        public TEvent GetEvent<TEvent>() where TEvent : EventBase, new()
        {
            if (!_events.TryGetValue(typeof(TEvent), out var @event))
                lock (_lockObject)
                    if (!_events.TryGetValue(typeof(TEvent), out @event))
                    {
                        @event = new TEvent();
                        _events[typeof(TEvent)] = @event;
                    }

            return (TEvent)@event;
        }
    }
}
