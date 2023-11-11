using _Scripts.Enums;
using UnityEngine;

namespace _Scripts.UI
{
    public class UIWindow : MonoBehaviour
    {
        [field: SerializeField] public eWindowType windowType { get; private set; } = eWindowType.None;
        [SerializeField] public bool disableOnChangeFocus = true;
        [SerializeField] public bool requireAllDisableOnFocus = true; 
        
        public void Show() { gameObject.SetActive(true); }
        public void Hide() { gameObject.SetActive(false); }
    }
}
