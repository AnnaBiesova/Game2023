using System.Collections;
using _Scripts.UI;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public sealed class WinWindow : UIWindow
{
    [SerializeField] private CanvasConfig config;
    [SerializeField] private GameObject nextLevelButton;
    [SerializeField] private Image backgroundImage;
    
    private bool delayedOnce;

    private async void OnEnable()
    {
        if (delayedOnce == false)
        {
            float delayShowingButton = config.DelayShowingNextLevelButton;
            
            Color fromBackgroundColor = backgroundImage.color;
            fromBackgroundColor.a = 0f;
            backgroundImage.DOColor(fromBackgroundColor, delayShowingButton).From();
            
            nextLevelButton.SetActive(false);
            delayedOnce = true;
            
            StartCoroutine(Wait(delayShowingButton));
            
            //DelayAction.WaitForSecondsRealtime(() => nextLevelButton.SetActive(true), config.DelayShowingNextLevelButton).Forget();

        }
    }

    IEnumerator Wait(float delayShowingButton)
    {
        yield return new WaitForSeconds(delayShowingButton);

        nextLevelButton.SetActive(true);
        //Tween button scale from zero to 1
        nextLevelButton.transform.DOScale(Vector3.zero, delayShowingButton * 0.5f).SetEase(Ease.OutBack).From();
    }
}
