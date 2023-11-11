using UnityEngine;

public sealed class CanvasCameraSetter : MonoBehaviour
{
    [SerializeField] private string cameraTag = "MainCamera";

#if UNITY_EDITOR
    private string[] GetAllTags()
    {
        return UnityEditorInternal.InternalEditorUtility.tags;
    }
#endif
    
    private void Awake()
    {
        GetComponent<Canvas>().worldCamera = GameObject.FindWithTag(cameraTag).GetComponent<Camera>();
    }
}
