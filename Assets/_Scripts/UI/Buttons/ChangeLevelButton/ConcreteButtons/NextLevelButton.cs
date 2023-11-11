using _Configs.ScriptableObjectsDeclarations.Configs;
using _Scripts.Controllers;
using _Scripts.Patterns.Events;

namespace _Scripts.UI
{
    public sealed class NextLevelButton : ChangeLevelButton.ChangeLevelButton
    {
        private OtherSettingsSO otherSettingsSo;

        protected override void Awake()
        {
            base.Awake();

            otherSettingsSo = OtherSettingsSO.Instance;
        }

        protected override bool ChangeLevel()
        {
            if (base.ChangeLevel() == false) return false;

            LevelManager.SetLevelRestartedState(false);

            if (otherSettingsSo.SetProgressType == OtherSettingsSO.eSetProgressType.onNextLeveButton)
            {
                SaveManager.AddLevelToProgress();
            }
            
            LevelOrder.Instance.LoadNextGameLevel(OtherSettingsSO.Instance.DelayLoadingNextLevel);
            
            return true;
        }
    }
}
