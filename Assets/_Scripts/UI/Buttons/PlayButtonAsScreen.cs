using _Scripts.Controllers;
using _Scripts.Enums;
using _Scripts.Patterns.Events;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _Scripts.UI
{
    public sealed class PlayButtonAsScreen : MonoBehaviour, IPointerDownHandler
    {
        [SerializeField] private Transform parentAfterPress;
    
        private bool started;
    
        public void OnPointerDown(PointerEventData eventData)
        {
            if(started) return;
            started = true;
            
            VibrationController.Instance.Light();

            transform.SetParent(parentAfterPress, false);
            this.OnEvent(EventID.LEVEL_START);
        }
    }
}
