using System;

namespace _Scripts.UI
{
	public class PopUpWindowCloseButton : BaseButton
	{
		private Action onClick;

		public void Init(Action onClick)
		{
			this.onClick = onClick;
		}

		protected override void OnClick()
		{
			base.OnClick();

			onClick?.Invoke();
		}
	}
}