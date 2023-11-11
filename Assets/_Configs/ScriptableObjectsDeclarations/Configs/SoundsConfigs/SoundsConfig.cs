using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _Scripts.Enums;
using _Scripts.Patterns;
using UnityEngine;

namespace _Configs.ScriptableObjectsDeclarations.Configs.SoundsConfigs
{
    [CreateAssetMenu(fileName = "SoundsConfig", menuName = "Pawsome/Game Settings/Sounds/SoundsConfig", order = 0)][Serializable]
    public sealed class SoundsConfig : SingletonScriptableObject<SoundsConfig>
    {
        [SerializeField] private string pathToFolderWithInGameSoundsConfigs;

        private List<InGameSoundConfig> soundParamsList = new List<InGameSoundConfig>();

        public string PathToFolderWithInGameSoundsConfigs => pathToFolderWithInGameSoundsConfigs;

        protected override void OnInitialize()
        {
            List<string> path = pathToFolderWithInGameSoundsConfigs.Split('/').ToList();
            int indexOfResources = path.IndexOf("Resources");
            StringBuilder correctPath = new StringBuilder();

            for (int i = indexOfResources + 1; i < path.Count; i++)
            {
                correctPath.Append(path[i]);
                if(i != path.Count - 1) correctPath.Append('/');
            }
            soundParamsList = Resources.LoadAll<InGameSoundConfig>(correctPath.ToString()).ToList();
        }

        public InGameSoundConfig GetSoundParams(eSoundType soundType)
        {
            if(soundParamsList.Count == 0) OnInitialize();
            return soundParamsList.Find(s => s.SoundType == soundType);
        }
    }
}


    