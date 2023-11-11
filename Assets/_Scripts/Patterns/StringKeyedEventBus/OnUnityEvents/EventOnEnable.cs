namespace _Scripts.Patterns.Events.OnUnityEvents
{
	public class EventOnEnable : OnUnityEventBase
	{
		private void OnEnable()
		{
			eventToCall?.Invoke();
		}
	}
}