using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Patterns.Events
{
    public sealed class EventController
    {
        private List<ListenerData> _listeners;
        
        public bool IsEmpty => _listeners == null || _listeners.Count == 0;

        public EventController()
        {
            this._listeners = new List<ListenerData>();
        }

        public void RegisterListener(ListenerData listenerData)
        {
            _listeners.Add(listenerData);
        }

        public void RemoveListener(ListenerData listenerData)
        {
            _listeners.Remove(listenerData);
        }

        public void Call(object param)
        {
            for (int i = _listeners.Count - 1; i >= 0; i--)
            {
                _listeners[i].Callback.Call(param);
            }
        }
    }
}
