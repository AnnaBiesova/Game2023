using _Configs.ScriptableObjectsDeclarations.Configs;
using _Scripts.Controllers;
using _Scripts.Enums;
using _Scripts.Patterns.Events;
using _Scripts.Services;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Scripts.UI.DebugFeatures.Buttons
{
    public sealed class ScrollLevelButton : BaseDebugButton
    {
        [SerializeField] eDirrection dirrection;

        protected override void OnClick()
        {
            SetPrefsName();
            
            base.OnClick();

            LevelManager.SetLevelRestartedState(false);
            
            if (dirrection == eDirrection.right)
            {
                SaveManager.AddLevelToProgress();
            }
            else 
            {
                SaveManager.RemoveLevelFromProgress();
            }
            
            LevelOrder.Instance.LoadNextGameLevel();
        }
        
        protected override void SetPrefsName()
        {
            prefsDebugParamName = DebugEnableableFeaturesPrefsNames.DEBUG_SCROLL_LEVELS;
        }


    }
}
