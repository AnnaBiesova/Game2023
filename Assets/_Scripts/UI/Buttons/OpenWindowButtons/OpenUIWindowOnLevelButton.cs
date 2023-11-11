using _Scripts.Controllers;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
	public class OpenUIWindowOnLevelButton : OpenUIWindowButton
	{
		[SerializeField] private Text openOnLevelText;
		[SerializeField] private string openOnLevelPrefix;
		[SerializeField] private GameObject restrictionGameObject;
		
		private int levelToOpen;
		private bool buttonIsAvailable;

		protected override void Awake()
		{
			base.Awake();

			//levelToOpen = OtherSettingsSO.Instance.LevelToOpenItemsUI;
			buttonIsAvailable = SaveManager.LevelForPlayer >= levelToOpen;

			if (buttonIsAvailable)
			{
				restrictionGameObject.SetActive(false);
			}
			else
			{
				openOnLevelText.text = openOnLevelPrefix + levelToOpen;
			}
		}

		protected override void OnClick()
		{
			if(buttonIsAvailable) base.OnClick();
		}
	}
}