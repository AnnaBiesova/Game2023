using _Scripts.Controllers;
using UnityEngine;
using UnityEngine.UI;

public sealed class GoalTextSetter : MonoBehaviour
{
    [SerializeField] private Text text;

    private void Start()
    {
        //text.text = LevelManager.Instance.LevelConfig.LevelRules.ruleConfig.TutorialText;
    }
}
