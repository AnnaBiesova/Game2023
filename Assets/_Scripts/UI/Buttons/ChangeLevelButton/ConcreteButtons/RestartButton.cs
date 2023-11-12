using _Configs.ScriptableObjectsDeclarations.Configs;
using _Scripts.Controllers;
using DG.Tweening;
using UnityEngine.SceneManagement;

namespace _Scripts.UI
{
    public sealed class RestartButton : ChangeLevelButton.ChangeLevelButton
    {
        protected override bool ChangeLevel()
        {
            if(base.ChangeLevel() == false) return false;

            RestartLevel();
            
            return true;
        }

        public static void RestartLevel()
        {
            DOTween.KillAll();

            LevelManager.SetLevelRestartedState(true);

            //LevelOrder.Instance.LoadHubScene();
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }
    }
}
