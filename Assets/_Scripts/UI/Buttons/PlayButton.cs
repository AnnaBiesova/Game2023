using _Scripts.Patterns.Events;

namespace _Scripts.UI
{
	public class PlayButton : BaseButton
	{
		protected override void OnClick()
		{
			base.OnClick();

			this.OnEvent(EventID.LEVEL_START);
		}
	}
}