using System;
using System.Collections.Generic;

namespace _Scripts.Patterns.Events
{
    public sealed class EventTypedCallbackData<T> : BaseCallbackData
    {
        private readonly Action<T> _typedEventCallback;

        public EventTypedCallbackData(Action<T> callback) => this._typedEventCallback = callback;

        public override void Call(object param)
        {
            this._typedEventCallback?.Invoke((T) param);
        }

        private bool Equals(EventTypedCallbackData<T> other) => Equals(_typedEventCallback, other._typedEventCallback);

        public override bool Equals(object obj) => ReferenceEquals(this, obj) || obj is EventTypedCallbackData<T> other && Equals(other);

        public override int GetHashCode() => (_typedEventCallback != null ? _typedEventCallback.GetHashCode() : 0);

        private sealed class TypedEventCallbackEqualityComparer : IEqualityComparer<EventTypedCallbackData<T>>
        {
            public bool Equals(EventTypedCallbackData<T> x, EventTypedCallbackData<T> y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return Equals(x._typedEventCallback, y._typedEventCallback);
            }

            public int GetHashCode(EventTypedCallbackData<T> obj)
            {
                return (obj._typedEventCallback != null ? obj._typedEventCallback.GetHashCode() : 0);
            }
        }

        public static IEqualityComparer<EventTypedCallbackData<T>> typedEventCallbackComparer { get; } = new TypedEventCallbackEqualityComparer();
    }

    
}
