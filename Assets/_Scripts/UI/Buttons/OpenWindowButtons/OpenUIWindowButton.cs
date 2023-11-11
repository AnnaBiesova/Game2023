using _Scripts.Enums;
using UnityEngine;

namespace _Scripts.UI
{
	public class OpenUIWindowButton : BaseButton
	{
		[SerializeField] private eWindowType windowType;
		[SerializeField] [Min(0)] private float showWindowDelay = 0f;
		
		protected override void OnClick()
		{
			base.OnClick();

			if (showWindowDelay != 0)
			{
				UIManager.Instance.ShowWindow(windowType, showWindowDelay);
			}
			else
			{
				UIManager.Instance.ShowWindow(windowType);
			}
		}
	}
}