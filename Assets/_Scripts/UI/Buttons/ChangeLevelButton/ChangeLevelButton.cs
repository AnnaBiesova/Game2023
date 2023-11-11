using System;
using _Scripts.Controllers;

namespace _Scripts.UI.ChangeLevelButton
{
	public class ChangeLevelButton : BaseButton
	{
		private bool pressed = false;

		public static event Action OnChangeLevelButtonPress; 

		protected override void OnClick()
		{
			base.OnClick();

			ChangeLevel();
		}
		
		protected virtual bool ChangeLevel()
		{
			if (pressed) return false;
			pressed = true;
			
			OnChangeLevelButtonPress?.Invoke();
			
			return true;
		}


	}
}