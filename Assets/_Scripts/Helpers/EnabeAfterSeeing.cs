using UnityEngine;

public class EnabeAfterSeeing : MonoBehaviour
{
    [SerializeField] private int hideTimesCountBeforeShow = 1;

    private string PrefName => gameObject.name + "_hideCount";
    
    private void OnEnable()
    {
        int hideCount = PlayerPrefs.GetInt(PrefName, 0);

        if (hideCount < hideTimesCountBeforeShow)
        {
            PlayerPrefs.SetInt(PrefName, hideCount + 1);
            gameObject.SetActive(false);
        }
    }
}
