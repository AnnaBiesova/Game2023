using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Configs.ScriptableObjectsDeclarations.Configs.GameModesConfigs
{
    [Serializable]
    [CreateAssetMenu(fileName = "Default_GameModeConfig", menuName = "Pawsome/Game Mode Configs/DefaultGameModeConfig",
        order = 0)]
    public class GameModeConfig : ScriptableObject
    {
        public bool ShowProgressBar = true;
        [SerializeField] private bool allowAllScenes;
        

        [SerializeField] private List<string> allowedScenes;

        
        public IEnumerable<string> GetAllowedScenes()
        {
            return allowAllScenes ? GetScenesInBuildSettingsNames() : allowedScenes;
        }
        
        private List<string> GetScenesInBuildSettingsNames()
        {
            return new List<string>();
        }
    }
}