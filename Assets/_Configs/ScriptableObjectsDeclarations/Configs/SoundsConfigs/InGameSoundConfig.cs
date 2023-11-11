using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Enums;
using _Scripts.Extensions;
using UnityEngine;
using UnityEngine.Audio;

namespace _Configs.ScriptableObjectsDeclarations.Configs.SoundsConfigs
{
    [Serializable] [CreateAssetMenu(fileName = "InGameSoundConfig", menuName = "Pawsome/Game Settings/Sounds/InGameSoundConfig", order = 0)]
    public class InGameSoundConfig : ScriptableObject
    {
        [SerializeField] private eSoundType soundType;
        [SerializeField] private AudioMixerGroup mixer;
        
        [SerializeField] private List<AudioClip> clips;
       
        [SerializeField] private Vector2 randomPitchRange;
        [SerializeField] private float defaultVolume = 0.5f;
        [SerializeField] private float volumeForceMultiplayer;
    
        public string PathToConfigs => SoundsConfig.Instance.PathToFolderWithInGameSoundsConfigs;
    
        public Vector2 RandomPitchRange => randomPitchRange;
        public float DefaultVolume => defaultVolume;
        public float VolumeForceMultiplayer => volumeForceMultiplayer;

        public eSoundType SoundType => soundType;

        public AudioClip GetRandomClip()
        {
            clips.Shuffle();
            return clips.First();
        }
    }
}
