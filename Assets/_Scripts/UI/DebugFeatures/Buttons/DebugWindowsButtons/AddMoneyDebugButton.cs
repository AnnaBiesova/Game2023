using _Scripts.Controllers;
using _Scripts.Services;

namespace _Scripts.UI.DebugFeatures.Buttons
{
	public class AddMoneyDebugButton : BaseDebugButton
	{
		protected override void OnClick()
		{
			base.OnClick();

			SaveManager.ChangePlayerMoney(OtherSettingsSO.Instance.AddMoneyButtonAmount, false);
		}

		protected override void SetPrefsName()
		{
			prefsDebugParamName = DebugEnableableFeaturesPrefsNames.DEBUG_ADD_MONEY;
		}
	}
}