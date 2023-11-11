using System;
using System.Collections.Generic;
using System.Linq;
using _Configs.ScriptableObjectsDeclarations.Configs.SoundsConfigs;
using _Scripts.Enums;
using _Scripts.Extensions;
using _Scripts.Patterns;
using _Scripts.Patterns.Events;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public sealed class SoundController : Singleton<SoundController>
{
    /// <summary>
    /// Used for override global sound config for level
    /// </summary>
    [Serializable]
    public class AudioClipsSetForLevel
    {
        [SerializeField] private eSoundType soundType;
        [SerializeField] private List<AudioClip> _clips;

        public eSoundType SoundType => soundType;
        public AudioClip GetRandomClip() => _clips.Shuffle().First();
    }
    
    [SerializeField] private AudioSource backgroundMusicSource;
    [SerializeField] private AudioSource buttonPressSource;
    [SerializeField] private AudioSource miningSource;

    [SerializeField]
    private List<AudioClipsSetForLevel> clipSetsForOverride = new List<AudioClipsSetForLevel>();

    private void OnEnable()
    {
        this.Subscribe(EventID.LEVEL_FAIL, OnLevelEnd);
        this.Subscribe(EventID.LEVEL_DONE, OnLevelEnd);
    }

    private void OnDisable()
    {
        this.Unsubscribe(EventID.LEVEL_FAIL, OnLevelEnd);
        this.Unsubscribe(EventID.LEVEL_DONE, OnLevelEnd);
    }

    /*private void Awake()
    {
        PlaySound(eSoundType.backgroundMusic);
    }*/

    private void OnLevelEnd()
    {
        SetSourceVolume(eSoundType.backgroundMusic, 0f, 1f);
    }
    
    
    public void PlaySound(eSoundType soundType, float force = 1)
    {
        /*if(SettingsController.SavedData.soundEnabled == false && soundType != eSoundType.backgroundMusic) return;
        if(SettingsController.SavedData.musicEnabled == false && soundType == eSoundType.backgroundMusic) return;*/
        
        if (soundType == eSoundType.none)
        {
            return;
        }

        AudioSource source = PickSource(soundType);
        if(source == null || source.isPlaying) return;
        
        InGameSoundConfig soundParams = SoundsConfig.Instance.GetSoundParams(soundType);

        if (soundParams == null)
        {
            Debug.LogError("Can't find correct sound params GetParams returned null");
            return;
        }
        
        float randomPitch = Random.Range(soundParams.RandomPitchRange.x, soundParams.RandomPitchRange.y);
        float forceMultiplayer = soundParams.VolumeForceMultiplayer;

        AudioClipsSetForLevel clipsOverrideGlobalConfig =
            clipSetsForOverride.Find(clipsSet => clipsSet.SoundType == soundType);
        AudioClip clipToPlay = clipsOverrideGlobalConfig != null ? clipsOverrideGlobalConfig.GetRandomClip() : soundParams.GetRandomClip();

        float volume = Mathf.Lerp(0f, 1f, soundParams.DefaultVolume + forceMultiplayer * force);
        
        SetupSourceToPlay(source, randomPitch, volume, clipToPlay);
    }

    public void SetSourceVolume(eSoundType soundType, float volume, float blendTime)
    {
        AudioSource source = PickSource(soundType);

        if (blendTime <= 0)
        {
            source.volume = volume;
        }
        else
        {
            DOTween.To(x => source.volume = x, source.volume, volume, blendTime).timeScale = 1f;
        }
    }

    public bool IsPlaying(eSoundType soundType)
    {
        return PickSource(soundType).isPlaying;
    }
    

    private AudioSource PickSource(eSoundType soundType)
    {
        switch (soundType)
        {
            case eSoundType.backgroundMusic:
                return backgroundMusicSource;
            case eSoundType.buttonPress:
                return buttonPressSource;
            case eSoundType.mining:
                return miningSource;
            default:
                return null;
        }
    }

    private void SetupSourceToPlay(AudioSource source, float pitch, float volume, AudioClip clip)
    {
        source.pitch = pitch;
        source.volume = volume;
        source.clip = clip;
        
        source.Play();
    }
}
