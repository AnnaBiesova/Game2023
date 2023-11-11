using System;
using System.Collections.Generic;

namespace _Scripts.Patterns.Events
{
    public sealed class EventCallbackData : BaseCallbackData
    {
        private readonly Action _emptyEventCallback;

        public EventCallbackData(Action callback) => this._emptyEventCallback = callback;

        public override void Call(object param)
        {
            this._emptyEventCallback?.Invoke();
        }

        private bool Equals(EventCallbackData other) => Equals(_emptyEventCallback, other._emptyEventCallback);

        public override bool Equals(object obj) => ReferenceEquals(this, obj) || obj is EventCallbackData other && Equals(other);

        public override int GetHashCode() => (_emptyEventCallback != null ? _emptyEventCallback.GetHashCode() : 0);

        private sealed class EmptyEventCallbackEqualityComparer : IEqualityComparer<EventCallbackData>
        {
            public bool Equals(EventCallbackData x, EventCallbackData y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return Equals(x._emptyEventCallback, y._emptyEventCallback);
            }

            public int GetHashCode(EventCallbackData obj)
            {
                return (obj._emptyEventCallback != null ? obj._emptyEventCallback.GetHashCode() : 0);
            }
        }

        public static IEqualityComparer<EventCallbackData> emptyEventCallbackComparer { get; } = new EmptyEventCallbackEqualityComparer();
    }
}
