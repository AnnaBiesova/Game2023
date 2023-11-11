using UnityEngine;

namespace _Scripts.Controllers.Rules
{
    public class LevelProgressInfo
    {
        public bool ShowProgressBar = true;
        
        private bool levelInProgress = false;
        private float linearProgress = 0f;

        public float levelProgressInSeconds;

        public LevelProgressInfo(bool showProgressBar)
        {
            ShowProgressBar = showProgressBar;
        }
        
        public bool GetLevelInProgress() => levelInProgress;
        public float GetLinearProgress() => linearProgress;

        public void SetLevelInProgress(bool inProgress)
        {
            levelInProgress = inProgress;
        }
        
        public void SetLinearProgress(float linearProgress)
        {
            this.linearProgress = Mathf.Min(linearProgress, 1f);
        }
    }
}