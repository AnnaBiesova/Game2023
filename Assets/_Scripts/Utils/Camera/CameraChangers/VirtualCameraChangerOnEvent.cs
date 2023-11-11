using System.Collections.Generic;
using _Scripts.Patterns.Events;
using UnityEngine;

namespace _Scripts.Utils.Camera
{
    public sealed class VirtualCameraChangerOnEvent : VirtualCameraChanger
    {
        [SerializeField] private string eventId;
        
        public IEnumerable<string> GetAllEventsNames()
        {
            return EventID.GetAllEventsNames();
        }
        
        private void OnEnable()
        {
            this.Subscribe(eventId, ChangeCameraWithBlend);
        }
        
        protected override void OnDestroy()
        {
            base.OnDestroy();
            
            this.Unsubscribe(eventId, ChangeCameraWithBlend);
        }
    }
}
