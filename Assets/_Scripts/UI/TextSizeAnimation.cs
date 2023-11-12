using UnityEngine;
using UnityEngine.UI;

public class TextSizeAnimation : MonoBehaviour
{
    [SerializeField] private Text text;
    [Space]
    [SerializeField] private int fromSize = 10;
    [SerializeField] private int toSize = 15;
    [Space]
    [SerializeField] private float duration = 0.5f;
    [SerializeField] private float speed = 1f;

    private void Update()
    {
        text.fontSize = (int)Mathf.Lerp(fromSize, toSize, Mathf.PingPong(Time.time * speed, duration) / duration);
    }
}
