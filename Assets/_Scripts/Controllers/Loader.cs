 using System;
 using System.Collections.Generic;
 using _Configs.ScriptableObjectsDeclarations.Configs;
 using _Scripts.Patterns;
using _Scripts.Services;
 using UnityEngine;

namespace _Scripts.Controllers
{
    [AutoCreateSingelton]
    public sealed class Loader : Singleton<Loader>
    {
        [SerializeField] private bool loadHub;
        
        private void Awake()
        {
            if (OtherSettingsSO.Instance == null)
            {
                Debug.Log($"Other settings inited {OtherSettingsSO.Instance.name}");
            }
            

            SetNumVoices(1);
            
            DontDestroyOnLoad(gameObject);

            Taptic.tapticOn = true;
        }

        private void Start()
        {
            if (!IsCreatedOnLevel)
            {
                if (loadHub)
                {
                    LevelOrder.Instance.LoadHubScene();   
                }
                else
                {
                    LevelOrder.Instance.LoadNextGameLevel();
                }
            }
        }


        //Audio optimisation for decreasing number of active voices; from: https://youtu.be/W45-fsnPhJY?t=1964
        public void SetNumVoices(int nv)
        {
            var config = AudioSettings.GetConfiguration();
            if(config.numVirtualVoices == nv) 
                return;

            config.numVirtualVoices = nv;
            config.numRealVoices = Mathf.Clamp(config.numRealVoices, 1, config.numVirtualVoices);

            AudioSettings.Reset(config);
        }
    }
}
