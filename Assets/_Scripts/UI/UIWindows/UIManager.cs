using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Controllers;
using _Scripts.Enums;
using _Scripts.Patterns;
using _Scripts.Patterns.Events;
using Cinemachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Scripts.UI
{
    public sealed class UIManager : Singleton<UIManager>
    {
        [Serializable]
        private class LevelNumberTextAndPrefix
        {
            public Text text;
            public string prefix;
        }

        [SerializeField] private CanvasConfig config;

        [Space]
        [SerializeField] private List<LevelNumberTextAndPrefix> levelNumberTextAndPrefix;
        
        private UIWindow currentWindow;
        private Dictionary<eWindowType, UIWindow> allWindows = new Dictionary<eWindowType, UIWindow>();
        private Canvas canvas;


        public  eWindowType currentWindowType = eWindowType.None;
        public  eWindowType previousWindowType = eWindowType.None;
        
        private void Awake()
        {
            canvas = GetComponent<Canvas>();
            
            EventSystem[] es = FindObjectsOfType<EventSystem>();
            if(es.Length > 1) es[1].gameObject.SetActive(false);
            
            InitWindows();

            foreach (var textAndPrefix in levelNumberTextAndPrefix)
            {
                textAndPrefix.text.text = $"{textAndPrefix.prefix} {SaveManager.LevelForPlayer}";
            }
        }

        private void Start()
        {
            ShowWindow(config.StartingWindow);
        }

        private void OnEnable()
        {
            this.Subscribe(EventID.LEVEL_START, OnLevelStart);
            this.Subscribe(EventID.LEVEL_FAIL, OnLevelFail);
            this.Subscribe(EventID.LEVEL_DONE, OnLevelDone);

        }
        
        private void OnDestroy()
        {
            this.Unsubscribe(EventID.LEVEL_START, OnLevelStart);
            this.Unsubscribe(EventID.LEVEL_FAIL, OnLevelFail);
            this.Unsubscribe(EventID.LEVEL_DONE, OnLevelDone);
        }
        private void OnLevelStart()
        {
            ShowWindow(eWindowType.Game);

        }

        private void OnLevelDone()
        {
            VibrationController.Instance.Success();
            
            canvas.planeDistance = 1f;
            StartCoroutine(ShowWindowWithDealy(config.DelayShowingWinWindow, eWindowType.Win));
        }
        private void OnLevelFail()
        {
            VibrationController.Instance.Failure();
            StartCoroutine(ShowWindowWithDealy(config.DelayShowingLoseWindow, eWindowType.Lost));
        }
        

        private IEnumerator ShowWindowWithDealy(float delay, eWindowType windowType) {
            yield return new WaitForSeconds(delay);
            ShowWindow(windowType);
        }
        
        private void InitWindows() {
            for (int i = 0; i < transform.childCount; i++)
            {
                var window = transform.GetChild(i).GetComponent<UIWindow>();
                if (window != null) {
                    if (allWindows.ContainsKey(window.windowType))
                    { 
                        Debug.LogError($"Found duplicate uiWindow - {window.windowType}. Change window type");
                        continue;
                    }

                    allWindows.Add(window.windowType, window);
                    window.Hide();
                }
            }    
        }

        public void ShowPreviousWindow(float delay = 0f)
        {
            if (previousWindowType == eWindowType.None) return;

            if (delay != 0)
            {
                ShowWindow(previousWindowType, delay);
            }
            else
            {
                ShowWindow(previousWindowType);
            }

        }
        
        public void ShowWindow(eWindowType windowType) 
        {
            if (currentWindow != null)
            {
                if (windowType == currentWindow.windowType) return;
            }

            UIWindow nextWindow = allWindows[windowType];

            if (nextWindow == null)
            {
                Debug.LogWarning("Window type is not presented: " + windowType);
                return;
            }
            
            if (currentWindow)
            {
                previousWindowType = currentWindow.windowType;
                if (currentWindow.disableOnChangeFocus || nextWindow.requireAllDisableOnFocus)
                {
                    currentWindow.Hide();
                }
            }

            currentWindowType = windowType;
            nextWindow.Show();
            currentWindow = nextWindow;
        }

        public void ShowWindow(eWindowType windowType, float delay)
        {
            StartCoroutine(ShowWindowWithDelayRoutine(windowType, delay));
        }

        public IEnumerator ShowWindowWithDelayRoutine(eWindowType windowType, float delay)
        {
            yield return new WaitForSecondsRealtime(delay);
            ShowWindow(windowType);
        }
    }
}
