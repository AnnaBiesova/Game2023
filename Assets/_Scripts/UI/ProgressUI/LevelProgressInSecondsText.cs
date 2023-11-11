using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
	public class LevelProgressInSecondsText : MonoBehaviour
	{
		[SerializeField] private Text secondsText;
		[SerializeField] private string timeFormat;
		[SerializeField] private bool update;
		
		private void OnEnable()
		{
			SetProgressInSeconds();
		}

		private void Update()
		{
			if(update == false) return;
			
			SetProgressInSeconds();
		}

		private void SetProgressInSeconds()
		{
			/*float progressInSeconds = (HouseOfTrapsRule.Instance as HouseOfTrapsRule).GetLevelProgressInSeconds();
			TimeSpan ts = TimeSpan.FromSeconds(progressInSeconds);
			secondsText.text = ts.ToString(@$"mm\:ss");*/
		}
	}
}