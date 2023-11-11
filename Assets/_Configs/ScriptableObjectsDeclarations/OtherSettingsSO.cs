using System;
using System.Collections.Generic;
using _Scripts.Patterns;
using _Scripts.Services;
using UnityEngine;

[Serializable]
[CreateAssetMenu(fileName = "OtherSettingsSO", menuName = "Pawsome/Game Settings/OtherSettingsSO")]
public class OtherSettingsSO : SingletonScriptableObject<OtherSettingsSO>
{
    public enum eSetProgressType
    {
        none,
        onLevelDone,
        onNextLeveButton,
    }

    [SerializeField] private eSetProgressType setProgressType;
    [Space]
    [SerializeField] private float delayLoadingNextLevel;
    [SerializeField] private bool enablePostProcessing;
    [Space]
    [SerializeField] private float writingOnWallLifetime;
    [Space]
    [SerializeField] private List<string> activeDebugFeatures = new List<string>();

    [Space]
    public int AddMoneyButtonAmount = 10000;
   
    public eSetProgressType SetProgressType => setProgressType;
    
    public bool EnablePostProcessing => enablePostProcessing;
    public float DelayLoadingNextLevel => delayLoadingNextLevel;
    public float WritingOnWallLifetime => writingOnWallLifetime;
    
    protected override void OnInitialize()
    {
        base.OnInitialize();

        foreach (string debugFeaturesName in GetAllDebugFeaturesNames())
        {
            if (Instance.activeDebugFeatures.Contains(debugFeaturesName))
            {
                PlayerPrefs.SetInt(debugFeaturesName, 1);
            }
            else
            {
                PlayerPrefs.DeleteKey(debugFeaturesName);
            }
        }
    }


    public static IEnumerable<string> GetAllDebugFeaturesNames()
    {
        return DebugEnableableFeaturesPrefsNames.GetAllDebugFeaturesNames();
    }
}
