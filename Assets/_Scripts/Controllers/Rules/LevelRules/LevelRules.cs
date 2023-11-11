using System;
using _Configs.ScriptableObjectsDeclarations.Configs;
using _Configs.ScriptableObjectsDeclarations.Configs.GameModesConfigs;
using _Scripts.Patterns.Events;
using UnityEngine;


namespace _Scripts.Controllers.Rules
{
    [Serializable]
    public abstract class LevelRules<T> : LevelRulesBase where T : RulesInfoBase
    {
        public GameModeConfig ruleConfig;
        
        public override Type RulesInfoType => typeof(T);
        public override LevelProgressInfo LevelProgressInfo { get; protected set; }
        public override GameModeConfig RuleConfig => ruleConfig;

        [Header("Sets Dynamically")]
        [SerializeField] protected T info;

        private static T Info;
        private static bool InfoSetOnLevel;
        
        public static T GetRuleInfo()
        {
            if (InfoSetOnLevel == false)
            {
                return LevelOrder.Instance.GetLevelConfig(SaveManager.LevelForPlayer, false).info as T;
            }
            else
            {
                return Info;
            }
        }
        
        protected virtual void Awake()
        {
            this.Subscribe(EventID.LEVEL_START, OnLevelStart);
            this.Subscribe(EventID.LEVEL_FAIL, OnLevelFail);
            this.Subscribe(EventID.LEVEL_DONE, OnLevelDone);
        }

        protected virtual void OnDestroy()
        {
            this.Unsubscribe(EventID.LEVEL_START, OnLevelStart);
            this.Unsubscribe(EventID.LEVEL_FAIL, OnLevelFail);
            this.Unsubscribe(EventID.LEVEL_DONE, OnLevelDone);
        }
        
        protected virtual void OnLevelStart()
        {
            LevelProgressInfo.SetLevelInProgress(true);
        }

        protected virtual void OnLevelDone()
        {
            LevelProgressInfo.SetLevelInProgress(false);
            InfoSetOnLevel = false;
        }

        protected virtual void OnLevelFail()
        {
            LevelProgressInfo.SetLevelInProgress(false);
            InfoSetOnLevel = false;
        }
        
        public override void SetRules(RulesInfoBase info)
        {
            this.info = info as T;
            Info = this.info;
            InfoSetOnLevel = true;
            CreateLevelProgressInfo(ruleConfig.ShowProgressBar);
        }

        protected virtual void CreateLevelProgressInfo(bool useProgressBar)
        {
            LevelProgressInfo = new LevelProgressInfo(useProgressBar);
        }

        public override int GetCurrentLevelStage()
        {
            return 0;
        }
    }
}