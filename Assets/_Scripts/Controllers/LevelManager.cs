using System;
using System.Collections.Generic;
using _Configs.ScriptableObjectsDeclarations.Configs;
using _Configs.ScriptableObjectsDeclarations.Configs.LevelConfigs;
using _Scripts.Controllers.Rules;
using _Scripts.Patterns;
using _Scripts.Patterns.Events;
using _Scripts.Patterns.SharedData;
using _Scripts.Services;
using _Scripts.UI;
using UnityEngine;


namespace _Scripts.Controllers
{
    public sealed class LevelManager : Singleton<LevelManager>
    {
        private class SavedData
        {
            public bool levelIsRestarted = false;
            public bool alreadySentDoneEventOnThisLevel = false;
        }

        private static readonly string saveName = PrefsNames.LEVEL_MANAGER_SAVE;

        private static SavedData _savedData;
        private static SavedData savedData
        {
            get
            {
                if (_savedData == null) Load();
                return _savedData;
            }
        }

        public static void Save()
        {
            if (_savedData == null) Load();

            var savedStr = JsonUtility.ToJson(_savedData);
            PlayerPrefs.SetString(saveName, savedStr);
        }

        private static void Load()
        {
            if (PlayerPrefs.HasKey(saveName))
            {
                var savedStr = PlayerPrefs.GetString(saveName);
                _savedData = JsonUtility.FromJson<SavedData>(savedStr);
            }
            else
            {
                _savedData = new SavedData();
            }
        }
        
        [SerializeField] private bool isHubScene;

        public static bool LevelIsRestarted => savedData.levelIsRestarted;
        public static bool LevelDoneEventAlreadySent => savedData.alreadySentDoneEventOnThisLevel;
        
        public static int RewardForLevel;
        
        private BaseLevelConfig levelConfig;
        private OtherSettingsSO otherSettingsSo;

        private List<Action<SharedData>> waitingInitialize = new List<Action<SharedData>>();
        private SharedData data;

        public LevelRulesBase LevelRules { get; private set; }
        public BaseLevelConfig LevelConfig => levelConfig;

        private static bool levelInProgress = false;
        
        private bool levelEndSet = false;
        private bool levelResult = false;
        private bool haveRules = false;

        public bool IsHubScene => isHubScene;
        public bool LevelEndSet => levelEndSet;
        public bool LevelResult => levelResult;
        public static bool LevelInProgress => levelInProgress;

        private uint nextRandomSeed;

        private void Awake()
        {
            if (OtherSettingsSO.Instance == null)
            {
                Debug.Log($"Other settings inited {OtherSettingsSO.Instance.name}");
            }

            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            Input.multiTouchEnabled = false;

#if UNITY_EDITOR
            Application.targetFrameRate = -1;
            Application.runInBackground = true;
#else
            Application.targetFrameRate = 60;
#endif       
            
            Time.timeScale = 1f;

            levelConfig = LevelOrder.Instance.GetLevelConfig(SaveManager.LevelForPlayer, false);

            otherSettingsSo = OtherSettingsSO.Instance;
            
            CreateLevelRules();
            
            this.Subscribe<Action<SharedData>>(EventID.SHADED_DATA_REQUEST, HandleSharedDataRequest);
            
        }
        
        private void OnDestroy()
        {
            this.Unsubscribe<Action<SharedData>>(EventID.SHADED_DATA_REQUEST, HandleSharedDataRequest);
        }

        private void OnApplicationQuit()
        {
            string lastSessionTimeString = DateTime.UtcNow.ToBinary().ToString();
            PlayerPrefs.SetString(PrefsNames.LAST_SESSION_TIME, lastSessionTimeString);
            
            SetLevelRestartedState(true);
        }

        private void Start()
        {
            CreateLevelData();
            
            SaveManager.MoneyCollectedOnLevel = 0;
            SettingsButton.settingsOpened = false;

            levelInProgress = true;
            
            if(isHubScene == false) this.OnEvent(EventID.LEVEL_START);

            SetLevelRestartedState(true);
        }

        private void FixedUpdate()
        {
            if (Input.GetKeyDown(KeyCode.W)) SetLevelEnd(true);
            if (Input.GetKeyDown(KeyCode.R)) RestartButton.RestartLevel();

            if(haveRules == false) return;

            if (LevelRules.LevelProgressInfo.GetLevelInProgress() == true)
            {
                LevelRules.SetLevelProgressInfo();
                CheckLevelWin();
            }
        }
        
        private void HandleSharedDataRequest(Action<SharedData> param)
        {
            if (data == null) this.waitingInitialize.Add(param);
            else param.Invoke(data);
        }

        private void CreateLevelData()
        {
            this.data = SharedData.Generate();
            this.waitingInitialize.ForEach(x => x.Invoke(data));
            this.waitingInitialize.Clear();
        }

        public static void SetLevelInProgress(bool inProgress)
        {
            levelInProgress = inProgress;
        }
        
        private void CreateLevelRules()
        {
            LevelRules = Instantiate(levelConfig.LevelRules.LevelRulePrefab).GetComponent<LevelRulesBase>();

            if (LevelRules == null)
            {
                Debug.LogError($"Level rules is null with level config {levelConfig.name}");
                return;
            }
            
            LevelRules.SetRules(levelConfig.info);
            haveRules = true;
        }

        public LevelProgressInfo GetLevelProgressInfo()
        {
            return LevelRules.LevelProgressInfo;
        }

        public static void SetLevelRestartedState(bool restarted)
        {
            if (restarted == false)
            {
                savedData.alreadySentDoneEventOnThisLevel = false;
            }
            
            savedData.levelIsRestarted = restarted;
            Save();
        }

        private void CheckLevelWin()
        {
            if (LevelRules.CheckWinCondition())
            {
                SetLevelEnd(true);
            }
        }

        public void SetLevelEnd(bool win)
        {
            if(levelEndSet == true) return;
            levelEndSet = true;
            levelResult = win;
            
            /*RewardForLevel = win
                ? LevelConfig.WinMoneyReward
                : LevelConfig.LoseMoneyReward;*/

            this.OnEvent(win ? EventID.LEVEL_DONE : EventID.LEVEL_FAIL);

            savedData.alreadySentDoneEventOnThisLevel = true;
            Save();
            
            if (otherSettingsSo.SetProgressType == OtherSettingsSO.eSetProgressType.onLevelDone)
            {
                SaveManager.AddLevelToProgress();
            }
        }
    }
}
