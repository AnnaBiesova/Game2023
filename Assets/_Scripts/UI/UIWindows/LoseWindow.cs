using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.UI
{
	public class LoseWindow : UIWindow
	{
		[SerializeField] private CanvasConfig config;
		[SerializeField] private GameObject restartButton;
		[SerializeField] private Image backgroundImage;

		private bool delayedOnce;

		private async void OnEnable()
		{
			if (delayedOnce == false)
			{
				float delayShowingButton = 1f;

				Color fromBackgroundColor = backgroundImage.color;
				fromBackgroundColor.a = 0f;
				backgroundImage.DOColor(fromBackgroundColor, delayShowingButton).From();

				restartButton.SetActive(false);
				delayedOnce = true;

				await UniTask.WaitForSeconds(delayShowingButton);

				restartButton.SetActive(true);
				
				//Tween button scale from zero to 1
				restartButton.transform.DOScale(Vector3.zero, delayShowingButton * 0.5f)
					.SetEase(Ease.OutBack).From();

				//DelayAction.WaitForSecondsRealtime(() => nextLevelButton.SetActive(true), config.DelayShowingNextLevelButton).Forget();

			}
		}
	}
}