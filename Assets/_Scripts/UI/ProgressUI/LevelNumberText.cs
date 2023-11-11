using _Scripts.Controllers;
using UnityEngine;
using UnityEngine.UI;

public class LevelNumberText : MonoBehaviour
{
    [SerializeField] private Text text;
    [SerializeField] private string levelNumberPrefix = "Level ";
    
    private void Start()
    {
        text.text = levelNumberPrefix + SaveManager.LevelForPlayer;
    }
}
