using System;
using System.Collections.Generic;
using System.Reflection;
using _Scripts.Controllers.Rules;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;
#endif

namespace _Configs.ScriptableObjectsDeclarations.Configs.LevelConfigs
{
    [Serializable][CreateAssetMenu(fileName = "Level Config", menuName = "Jak/Level Configs/Level Config")]
    public class BaseLevelConfig : ScriptableObject
    {
        [SerializeField] protected LevelRulesBase rules;
        [SerializeField] private string scene;
        public int WinMoneyReward = 100;
        public int LoseMoneyReward;


        public RulesInfoBase info;
        
        public LevelRulesBase LevelRules => rules;

        private void OnValidate()
        {
            if (rules == null)
            {
                scene = String.Empty;
            }

            //if (info == null) info = new SnipePitunRuleInfo();
            
            if ((rules != null && info == null) || (rules != null && info != null && info.GetType() != rules.RulesInfoType))
            {
                Assembly? assembly = rules.RulesInfoType?.Assembly;
                if (assembly != null)
                {
                    info?.OnDestroyRule();
                    info = assembly.CreateInstance(rules.RulesInfoType.FullName) as RulesInfoBase;
                }
                else info = null;
                
                GC.Collect();
            }
            
            InitRuleInfo();
        }

        private void PlayThisLevel()
        {
#if UNITY_EDITOR
            LevelOrder.Instance.SetDebugLevelToLoad(this);

            /*if (Application.isPlaying)
            {
                SceneManager.LoadSceneAsync(ScenesDropdown.GetSceneNameFromIndex(0));
            }
            else
            {
                EditorSceneManager.OpenScene(ScenesDropdown.GetSceneNameFromIndex(0));
                EditorApplication.EnterPlaymode();
            }*/
#endif
        }

        
        private IEnumerable<string>? GetAllowedScenes()
        {
            return (rules == null || rules.RuleConfig == null) ? null : rules.RuleConfig.GetAllowedScenes();
        }
        
        public void SetScene(string sceneName)
        {
            scene = sceneName;
        }
        
        public string Scene => scene;

        private void InitRuleInfo()
        {
            if(info == null) return;

            info.Init(this);
        }
    }

}