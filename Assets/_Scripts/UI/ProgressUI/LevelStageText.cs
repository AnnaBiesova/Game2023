using System;
using _Scripts.Controllers;
using _Scripts.Patterns.Events;
using UnityEngine;
using UnityEngine.UI;

public class LevelStageText : MonoBehaviour
{
	[SerializeField] private Text text;
	[SerializeField] private string levelStagePrefix = "stage ";

	private void Awake()
	{
		this.Subscribe<int>(EventID.LEVEL_STAGE_STARTED, OnStageUpdated);
	}
	
	private void OnDestroy()
	{
		this.Unsubscribe<int>(EventID.LEVEL_STAGE_STARTED, OnStageUpdated);
	}

	private void OnEnable()
	{
		if (LevelManager.Instance.LevelRules != null)
		{
			OnStageUpdated(LevelManager.Instance.LevelRules.GetCurrentLevelStage());
		}
	}

	private void OnStageUpdated(int stageIndex)
	{
		text.text = levelStagePrefix + (stageIndex + 1).ToString();
	}
}
