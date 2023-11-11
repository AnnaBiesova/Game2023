using System;
using _Scripts.Controllers;
using _Scripts.Patterns.Events;
using UnityEngine;

namespace _Scripts.UI
{
	public class ShowNextLevelScreenButton : OpenUIWindowButton
	{
		protected override void Awake()
		{
			base.Awake();
            
			this.Subscribe(EventID.LEVEL_DONE, OnLevelDone);
		}

		private void OnDestroy()
		{
			this.Unsubscribe(EventID.LEVEL_DONE, OnLevelDone);
		}

		private void OnEnable()
		{
			bool show = LevelManager.Instance.IsHubScene == false && LevelManager.Instance.LevelEndSet &&
			            LevelManager.Instance.LevelResult;
			
			gameObject.SetActive(show);
		}

		private void OnLevelDone()
		{
			gameObject.SetActive(true);
		}
	}
}