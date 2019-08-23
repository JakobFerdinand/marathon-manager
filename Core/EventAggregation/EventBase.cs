using System;
using System.Collections.Generic;

namespace Core.EventAggregation
{
    public abstract class EventBase
    {
        private readonly List<Action> _subscriptions = new List<Action>();

        public IDisposable Subscribe(Action action)
        {
            if (action is null)
                throw new ArgumentNullException(nameof(action));

            _subscriptions.Add(action);
            return new Unsubscribe(() => _subscriptions.Remove(action));
        }

        private class Unsubscribe : IDisposable
        {
            private readonly Action _unsubscribe;

            public Unsubscribe(Action unsubscribe)
                => _unsubscribe = unsubscribe ?? throw new ArgumentNullException(nameof(unsubscribe));

            public void Dispose() => _unsubscribe();
        }
    }
}