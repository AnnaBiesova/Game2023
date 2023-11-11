using System;
using System.Collections.Generic;
using _Configs.ScriptableObjectsDeclarations.Configs.LevelConfigs;
using _Scripts.Controllers;
using _Scripts.Helpers;
using _Scripts.Patterns;
using _Scripts.Services;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

#if UNITY_EDITOR
#endif

namespace _Configs.ScriptableObjectsDeclarations.Configs
{
    [Serializable]
    [CreateAssetMenu(fileName = "LevelOrder", menuName = "Jak/Game Settings/LevelOrderSettings")]
    public class LevelOrder : SingletonScriptableObject<LevelOrder>
    {
        [SerializeField] private bool debugLog;
        
        [SerializeField] private string hubSceneName;
        
        [Min(1)] [SerializeField] private int levelToStartOnSecondLap = 6;
        [SerializeField] private bool randomizeSecondLapLevels = false;
        
        private BaseLevelConfig debugLoadLevelToTest;

        [Space]
        [SerializeField] private List<BaseLevelConfig> _levelConfigs;

        private int debugSceneToLoadBuildIndex = -1;
        [NonSerialized] private int lastLevelIndex = 0;

        private List<string> GetScenesInBuildSettingsNames()
        {
            return ScenesDropdown.GetScenesInBuildSettings();
        }

        protected override void OnInitialize()
        {
            base.OnInitialize();

            if (PlayerPrefs.HasKey(DebugInternalSelfControlledPrefsNames.DEBUG_LOAD_DEBUG_LEVEL) == false)
            {
                SetDebugLevelToLoad(null);
            }
            
            debugSceneToLoadBuildIndex = -1;
        }

        public BaseLevelConfig GetLevelConfig(int levelNumber, bool setNextLevel)
        {
#if UNITY_EDITOR
            if (debugLoadLevelToTest != null) return debugLoadLevelToTest;
#endif
            
            int levelConfigIndex = CalculateLevelCurrentConfigIndex(levelNumber, setNextLevel);
            
            if(setNextLevel) PlayerPrefs.SetInt(PrefsNames.LAST_LEVEL_CONFIG, levelConfigIndex);

            if (debugSceneToLoadBuildIndex != -1)
            {
                BaseLevelConfig debugConfig = Instantiate(_levelConfigs[levelConfigIndex]);
                debugConfig.SetScene(ScenesDropdown.GetSceneNameFromIndex(debugSceneToLoadBuildIndex));

                debugSceneToLoadBuildIndex = -1;
            
                return debugConfig;
            }

            if (debugLog)
            {
                Debug.LogWarning($"Current player level: {SaveManager.LevelForPlayer}");
                Debug.LogWarning($"Current level config index: {levelConfigIndex} and name: {_levelConfigs[levelConfigIndex].name}");
            }

            return _levelConfigs[levelConfigIndex];
        }

        public void SetDebugLevelToLoad(BaseLevelConfig levelConfig)
        {
            debugLoadLevelToTest = levelConfig;

            if (levelConfig == null) return;
            
            PlayerPrefs.SetInt(DebugInternalSelfControlledPrefsNames.DEBUG_LOAD_DEBUG_LEVEL, 1);
            Debug.LogWarning($"debugLoadLevelToTest = {debugLoadLevelToTest.name} _ {Time.time}");
        }

        public int CalculateLevelCurrentConfigIndex(int levelNumber, bool setNextLevel)
        {
            levelNumber--;
            int levelConfigIndex;
            
            if (randomizeSecondLapLevels && LevelManager.LevelIsRestarted)
            {
                levelConfigIndex = PlayerPrefs.GetInt(PrefsNames.LAST_LEVEL_CONFIG, 0);
            }
            else if (levelNumber >= _levelConfigs.Count)
            {
                if (randomizeSecondLapLevels)
                {
                    if (setNextLevel == true)
                    {
                        levelNumber = Random.Range(levelToStartOnSecondLap, _levelConfigs.Count);
                        levelNumber = lastLevelIndex == levelNumber ? levelToStartOnSecondLap - 1 : levelNumber;
                        lastLevelIndex = levelNumber;
                    }
                    else
                    {
                        levelNumber = lastLevelIndex;
                    }
                }
                else
                {
                    levelNumber = Mathf.Max(levelNumber - _levelConfigs.Count, 0); // minus first lap
                    int levelInLap =
                        levelNumber %
                        (_levelConfigs.Count - levelToStartOnSecondLap + 1); // config index in scope of lap
                    levelNumber = levelInLap + levelToStartOnSecondLap - 1; // add to level in lap shift from list start
                }

                levelConfigIndex = levelNumber;
            }
            else
            {
                levelConfigIndex = levelNumber;
            }

            return levelConfigIndex;
        }

        public void LoadNextGameLevel(float loadDelay = 0f)
        {
            DelayAction.WaitForSecondsRealtime(
                () =>
                {
                    OnBeforeLevelLoad();

                    SceneManager.LoadScene(LevelOrder.Instance.GetLevelConfig(SaveManager.LevelForPlayer, true).Scene);
                }, loadDelay).Forget();
        }

        public void LoadLevelByNumber(int loadLevelIndex)
        {
            OnBeforeLevelLoad();

            SceneManager.LoadScene(LevelOrder.Instance.GetLevelConfig(loadLevelIndex, true).Scene);
        }

        public void LoadHubScene(float loadDelay = 0f)
        {
            DelayAction.WaitForSecondsRealtime(
                () =>
                {
                    OnBeforeLevelLoad();
                    SceneManager.LoadScene(hubSceneName, LoadSceneMode.Single);
                }, loadDelay).Forget();
        }

        private void OnBeforeLevelLoad()
        {
            //Set level end if not set
            LevelManager.SetLevelInProgress(false);

            DOTween.KillAll();
        }
        
        public int GetLevelLap() => Mathf.CeilToInt(SaveManager.LevelForPlayer / (float)_levelConfigs.Count);

        public void SetDebugNextScene(int sceneIndex)  
        {
            debugSceneToLoadBuildIndex = sceneIndex;
        }

        public int GetCurrentSceneBuildIndex()
        {
            return SceneManager.GetActiveScene().buildIndex;
        }
    

        #region Odin Inspector

#if UNITY_EDITOR
        private void OnBeginListElement(int index)
        {
            //SirenixEditorGUI.BeginBox("Level: " + (index + 1));
        }

        private void OnEndListElement(int index)
        {
            //SirenixEditorGUI.EndBox();
        }
#endif


        #endregion
    }

    [Serializable]
    public class LoadAnyLevelParams
    {
        public bool load = false;
        [Range(1, 998)] public int level = 1;
    }
}