using _Scripts.Services;

namespace _Scripts.UI.DebugFeatures.Buttons
{
    public class OpenRuntimeInspectorButton : BaseDebugWindowButton
    {
        protected override void SetPrefsName()
        {
            prefsDebugParamName = DebugEnableableFeaturesPrefsNames.DEBUG_RUNTIME_INSPECTOR;
        }
    }
}