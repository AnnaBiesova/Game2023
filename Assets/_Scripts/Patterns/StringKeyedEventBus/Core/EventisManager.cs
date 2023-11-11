using System;
using UnityEngine;
using Object = System.Object;

namespace _Scripts.Patterns.Events
{
    public static class EventisManager
    {
        public static void Subscribe(Object listener, Action callback, string eventID)
        {
            EventCallbackData callbackData = new EventCallbackData(callback);
            Subscribe(listener, callbackData, eventID);
        }

        public static void Subscribe<T>(Object listener, Action<T> typedCallback, string eventID)
        {
            EventTypedCallbackData<T> callbackData = new EventTypedCallbackData<T>(typedCallback);
            Subscribe(listener, callbackData, eventID);
        }

        /*public static void Subscribe<T, T1>(Object listener, Action<T, T1> typedCallback, string eventID)
        {
            EventTypedCallbackData<T, T1> callbackData = new EventTypedCallbackData<T, T1>(typedCallback);
            Subscribe(listener, callbackData, eventID);
        }*/

        public static void Unsubscribe(Object listener, Action callback, string eventID)
        {
            EventCallbackData callbackData = new EventCallbackData(callback);
            Unsubscribe(listener, callbackData, eventID);
        }

        public static void Unsubscribe<T>(Object listener, Action<T> typedCallback, string eventID)
        {
            EventTypedCallbackData<T> callbackData = new EventTypedCallbackData<T>(typedCallback);
            Unsubscribe(listener, callbackData, eventID);
        }

        private static void Subscribe(Object listener, BaseCallbackData callbackData, string eventID)
        {
            ListenerData listenerData = new ListenerData(listener, callbackData, eventID);
            EventisInternalManager.Subscribe(listenerData);
        }
        
        private static void Unsubscribe(Object listener, BaseCallbackData callbackData, string eventID)
        {
            ListenerData listenerData = new ListenerData(listener, callbackData, eventID);
            EventisInternalManager.Unsubscribe(listenerData);
        }

        public static void OnEvent(string eventID)
        {
            EventisInternalManager.OnEvent(eventID);
        }
        
        public static void OnEvent<T>(string eventID, T param)
        {
            EventisInternalManager.OnEvent(eventID, param);
        }
    }
}
