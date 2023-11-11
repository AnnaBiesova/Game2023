using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Patterns.Events
{
    public static class EventisInternalManager
    {
        private static Dictionary<string, EventController> _eventControllers;

        static EventisInternalManager()
        {
            _eventControllers = new Dictionary<string, EventController>();
        }

        public static void Subscribe(ListenerData listenerData)
        {
            if(!_eventControllers.ContainsKey(listenerData.EventID)) 
                _eventControllers.Add(listenerData.EventID, new EventController());

            _eventControllers[listenerData.EventID].RegisterListener(listenerData);
        }

        public static void Unsubscribe(ListenerData listenerData)
        {
            if(!_eventControllers.ContainsKey(listenerData.EventID)) return;
            
            _eventControllers[listenerData.EventID].RemoveListener(listenerData);
            if (_eventControllers[listenerData.EventID].IsEmpty) _eventControllers.Remove(listenerData.EventID);
        }

        public static void OnEvent(string eventID, object param = null)
        {
            if(_eventControllers.ContainsKey(eventID)) 
                _eventControllers[eventID].Call(param);
        }
    }
}
