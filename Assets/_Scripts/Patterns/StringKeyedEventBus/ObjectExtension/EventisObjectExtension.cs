using System;
using _Scripts.Patterns.Events;

public static class EventisObjectExtension
{
	public static void Subscribe(this Object listener, string EventID, Action callback) =>
		EventisManager.Subscribe(listener, callback, EventID);

	public static void Subscribe<T>(this Object listener, string EventID, Action<T> callback) =>
		EventisManager.Subscribe<T>(listener, callback, EventID);

	/*public static void Subscribe<T, T1>(this Object listener, string EventID, Action<T, T1> callback) =>
		EventisManager.Subscribe<T, T1>(listener, callback, EventID);

	public static void Unsubscribe<T, T1>(this Object listener, string EventID, Action<T, T1> callback) =>
		EventisManager.Unsubscribe<T, T1>(listener, callback, EventID);*/

	public static void Unsubscribe(this Object listener, string EventID, Action callback) =>
		EventisManager.Unsubscribe(listener, callback, EventID);

	public static void Unsubscribe<T>(this Object listener, string EventID, Action<T> callback) =>
		EventisManager.Unsubscribe<T>(listener, callback, EventID);

	public static void OnEvent(this Object sender, string EventID) => EventisManager.OnEvent(EventID);

	public static void OnEvent<T>(this Object sender, string EventID, T param) =>
		EventisManager.OnEvent<T>(EventID, param);
}