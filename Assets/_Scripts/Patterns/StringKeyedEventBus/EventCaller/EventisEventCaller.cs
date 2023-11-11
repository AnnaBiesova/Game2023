using System.Collections.Generic;
using _Scripts.Patterns.Events;
using UnityEngine;

public sealed class EventisEventCaller : MonoBehaviour
{
    public string eventId;
        
    public IEnumerable<string> GetAllEventsNames()
    {
        return EventID.GetAllEventsNames();
    }
    
    public void CallEventisEvent()
    {
        this.OnEvent(eventId);
    }
}



