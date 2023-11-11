using _Configs.ScriptableObjectsDeclarations.Configs;
using _Scripts.Helpers;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI.DebugFeatures.DebugLoadWindow
{
    public sealed class DebugLoadLevelDropdown : MonoBehaviour
    {
        private Dropdown _dropdown;

        private void Awake()
        {
            _dropdown = GetComponent<Dropdown>();
        
            _dropdown.options.Clear();
            
            foreach (var sceneName in ScenesDropdown.GetScenesInBuildSettings())
            {
                string optionName = sceneName.Split('_')[1];
                _dropdown.options.Add(new Dropdown.OptionData(optionName));
            }

            _dropdown.onValueChanged.AddListener(OnDropdownValueChanged);
        }

        private void OnDropdownValueChanged(int value)
        {
            // +1 is here for skip Loader (build index == 0) scene
            LevelOrder.Instance.SetDebugNextScene(value + 1);
        }
    }
}
