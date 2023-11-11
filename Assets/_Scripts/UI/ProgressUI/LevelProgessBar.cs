using System;
using _Scripts.Controllers;
using JoshH.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public sealed class LevelProgessBar : MonoBehaviour
{
    [SerializeField] private GameObject progressBarGo;
    [SerializeField] private Image progressImage;
    [SerializeField] private UIGradient progressbarGradient;
    [SerializeField] private Animator progressbarAnimator;

    private Gradient defaultLocationGradient;
    private Gradient inactiveLocationGradient;

    private bool lastEffectsState = true;
    private static readonly int Active = Animator.StringToHash("Active");

    private void Awake()
    {
        /*progressbarGradient.LinearGradient = defaultLocationGradient = ColorsConfig.Instance
            .GetPaletteBySceneName(SceneManager.GetActiveScene().name).ProgressbarBackgroundGradient;

        inactiveLocationGradient = ColorsConfig.Instance.inactiveProgressBarGradient;*/
    }

    private void LateUpdate()
    {
        /*bool showProgressBar = LevelManager.Instance.GetLevelProgressInfo().ShowProgressBar;

        progressBarGo.SetActive(showProgressBar);

        if (showProgressBar == false) return;*/

        if (LevelManager.Instance.GetLevelProgressInfo().GetLevelInProgress() == true)
        {
            SetProgress();
        }
    }
    
    public void SetEffectsActiveState(bool active)
    {
        if(lastEffectsState == active) return;
        lastEffectsState = active;

        progressbarGradient.LinearGradient = active ? defaultLocationGradient : inactiveLocationGradient;
        progressbarAnimator.SetInteger(Active, active ? 1 : 0);
    }
    
    
    private void SetProgress()
    {
        progressImage.fillAmount = Mathf.SmoothStep(progressImage.fillAmount, LevelManager.Instance.GetLevelProgressInfo().GetLinearProgress(), Time.unscaledDeltaTime * 10f);
    }
}
