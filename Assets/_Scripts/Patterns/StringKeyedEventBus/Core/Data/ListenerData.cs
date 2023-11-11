using System;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;


namespace _Scripts.Patterns.Events
{
    public sealed class ListenerData
    {
        private object Listener { get; }
        public BaseCallbackData Callback { get; }
        public string EventID { get; }

        public ListenerData(Object listener, BaseCallbackData callback, string eventID)
        {
            this.Listener = listener;
            this.Callback = callback;
            this.EventID = eventID;
        }

        
        private sealed class ListenerCallbackEventIDEqualityComparer : IEqualityComparer<ListenerData>
        {
            public bool Equals(ListenerData x, ListenerData y)
            {
                if (ReferenceEquals(x, y)) return true;
                if (ReferenceEquals(x, null)) return false;
                if (ReferenceEquals(y, null)) return false;
                if (x.GetType() != y.GetType()) return false;
                return Equals(x.Listener, y.Listener) && Equals(x.Callback, y.Callback) && x.EventID == y.EventID;
            }

            public int GetHashCode(ListenerData obj)
            {
                unchecked
                {
                    int hashCode = (obj.Listener != null ? obj.Listener.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (obj.Callback != null ? obj.Callback.GetHashCode() : 0);
                    hashCode = (hashCode * 397) ^ (obj.EventID != null ? obj.EventID.GetHashCode() : 0);
                    return hashCode;
                }
            }
        }

        public static IEqualityComparer<ListenerData> ListenerCallbackEventIDComparer { get; } = new ListenerCallbackEventIDEqualityComparer();

        private bool Equals(ListenerData other) => Equals(Listener, other.Listener) && Equals(Callback, other.Callback) && EventID == other.EventID;

        public override bool Equals(object obj) => ReferenceEquals(this, obj) || obj is ListenerData other && Equals(other);

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Listener != null ? Listener.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Callback != null ? Callback.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (EventID != null ? EventID.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}
