using _Configs.ScriptableObjectsDeclarations.Configs;
using _Scripts.Controllers;

namespace _Scripts.UI
{
	public class LoadHubSceneButton : ChangeLevelButton.ChangeLevelButton
	{
		protected override bool ChangeLevel()
		{
			/*if (base.ChangeLevel() == false) return false;

			LevelManager.SetLevelRestartedState(false);

			LevelOrder.Instance.LoadHubScene();

			return true;*/
			
			return true;
		}
	}
}