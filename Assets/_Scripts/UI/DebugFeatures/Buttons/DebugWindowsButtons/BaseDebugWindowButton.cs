using UnityEngine;

namespace _Scripts.UI.DebugFeatures.Buttons
{
    public abstract class BaseDebugWindowButton : BaseDebugButton
    {
        [SerializeField] private bool isEnabling;
        [SerializeField] private GameObject windowToControl;
        [SerializeField] private GameObject buttonToSwapTo;

        protected override void OnClick()
        {
            base.OnClick();
            
            SwapButtons();
        }

        private void SwapButtons()
        {
            SetWindowState();
            
            Transform newParent = buttonToSwapTo.transform.parent;
            
            buttonToSwapTo.transform.SetParent(transform.parent);
            buttonToSwapTo.transform.SetSiblingIndex(transform.GetSiblingIndex());

            transform.parent = newParent;
            
            buttonToSwapTo.SetActive(true);
            gameObject.SetActive(false);
        }

        private void SetWindowState()
        {
            windowToControl.SetActive(isEnabling);
        }
    }
}