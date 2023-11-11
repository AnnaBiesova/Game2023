using _Scripts.Core.Input;
using _Scripts.Patterns.Events;
using UnityEngine;

public class AimArrowController : MonoBehaviour
{
    public RectTransform _arrowRectTransform; // Assign the RectTransform of the arrow in the inspector
    
    private PlayerMovement _PlayerMovement;
    private Camera camera;

    private void Awake()
    {
        camera = Camera.main;
        _PlayerMovement = PlayerMovement.Instance;

        this.Subscribe<InputData>(EventID.INPUT, HandleInput);
        
        SetArrowActiveState(false);
    }

    private void OnDestroy()
    {
        this.Unsubscribe<InputData>(EventID.INPUT, HandleInput);
    }

    private void HandleInput(InputData data)
    {
        if (data.State == EInputState.Start)
        {
            PositionArrowAtPlayer();
        }

        if (data.State == EInputState.Continue)
        {
            CheckArrowActivationWithInputDirection(data.Direction);
            RotateArrow(data.Direction);
            ScaleArrowWithJumpForce();
        }

        if (data.State == EInputState.End || data.State == EInputState.None)
        {
            SetArrowActiveState(false);
        }
    }
    
    private void PositionArrowAtPlayer()
    {
        Vector2 screenPos = camera.WorldToScreenPoint(_PlayerMovement.transform.position);
        _arrowRectTransform.position = screenPos;
        SetArrowActiveState(true);
    }

    private void RotateArrow(Vector2 direction)
    {
        _arrowRectTransform.up = direction;
    }

    private void CheckArrowActivationWithInputDirection(Vector2 inputDir)
    {
        SetArrowActiveState(inputDir.y > 0f);
    }

    private void SetArrowActiveState(bool state)
    {
        _arrowRectTransform.gameObject.SetActive(state);
    }

    private void ScaleArrowWithJumpForce()
    {
        float jumpForceNormalized01 = _PlayerMovement.GetLastNormalizedJumpForce01();
        float yScale = 1f + Mathf.Lerp(0f, 4f, jumpForceNormalized01);
        
        _arrowRectTransform.localScale = new Vector3(1, yScale, 1);
    }
}
