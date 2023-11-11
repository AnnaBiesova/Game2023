using System;
using _Scripts.Controllers;

using UnityEngine;

namespace _Scripts.UI.DebugFeatures.Buttons
{
    public abstract class BaseDebugButton : BaseButton
    {
        protected string prefsDebugParamName;
        
        protected virtual void Start()
        {
            SetPrefsName();

            if (PlayerPrefs.HasKey(prefsDebugParamName) == false)
            {
                gameObject.SetActive(false);
            }
        }

        protected abstract void SetPrefsName();
    }
}
