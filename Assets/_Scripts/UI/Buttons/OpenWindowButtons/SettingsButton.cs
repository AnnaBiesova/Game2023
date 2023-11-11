using _Scripts.Enums;
using _Scripts.Services;
using UnityEngine;

namespace _Scripts.UI
{
    public sealed class SettingsButton : BaseButton
    {
        [SerializeField] private int tapsToOpenTestSuite = 10;
        [SerializeField] private float delayBetweenTapsToResetTapsCount = 0.5f;

        private bool enableTestSuite;
        private int tapsCount = 0;
        private float lastTapTime;

        public static bool settingsOpened = false;
        
        private void Start()
        {
            enableTestSuite = PlayerPrefs.HasKey(DebugEnableableFeaturesPrefsNames.DEBUG_ENABLE_TEST_SUITE);
        }

        protected override void OnClick()
        {
            base.OnClick();

            if (settingsOpened)
            {
                Time.timeScale = 1f;
                UIManager.Instance.ShowPreviousWindow();
            }
            else
            {
                Time.timeScale = 0f;
                UIManager.Instance.ShowWindow(eWindowType.Settings);
            }

            settingsOpened = !settingsOpened;

            if (enableTestSuite == false) return;

            if (Time.time - lastTapTime >= delayBetweenTapsToResetTapsCount) tapsCount = 0;

            tapsCount++;
            lastTapTime = Time.time;

            if (tapsCount >= tapsToOpenTestSuite)
            {
                tapsCount = 0;
            }
        }
    }
}


