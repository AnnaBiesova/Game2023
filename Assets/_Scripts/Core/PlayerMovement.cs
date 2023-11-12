using _Scripts;
using _Scripts.Controllers;
using _Scripts.Core.Input;
using _Scripts.Patterns;
using _Scripts.Patterns.Events;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class PlayerMovement : Singleton<PlayerMovement>
{
    private const float _makeIsKinematicDelay = 0.15f;
    private const float SetStableOnPlatformDuration = 0.2f;
    private const float JumpRotationDuration = 0.1f;
    
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _jumpForce = 50f;
    [SerializeField] private float _maxInputDeltaInViewportSpace = 0.25f;
    [SerializeField] private LayerMask _levelPlatformsLayerMask;
    
    private Camera _camera;
    
    private Vector2 _lastJumpForce;
    private float _lastNormalizedJumpForce01;
    private float _lastJumpTime;
    private bool _isInJumpProcess;
    
    public bool CanJump => LevelManager.LevelInProgress && _isInJumpProcess == false;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void OnEnable()
    {
        this.Subscribe<InputData>(EventID.INPUT, HandleInput);
    }

    private void OnDisable()
    {
        this.Unsubscribe<InputData>(EventID.INPUT, HandleInput);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_isInJumpProcess == false) return;
        
        if (other.gameObject.GetComponent<LevelPlatform>())
        {
            if (Time.time - _lastJumpTime < _makeIsKinematicDelay)
            {
                return;
            }

            if (HavePlatformToLandOn() == false)
            {
                return;
            }
            
            _rigidbody.isKinematic = true;
            SetPositionOnPlatform();
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if(_isInJumpProcess == false) return;
        
        if (other.gameObject.GetComponent<LevelPlatform>())
        {
            if (Time.time - _lastJumpTime < _makeIsKinematicDelay)
            {
                return;
            }

            if (HavePlatformToLandOn() == false)
            {
                return;
            }

            _rigidbody.isKinematic = true;
            SetPositionOnPlatform();
        }
    }

    private void HandleInput(InputData data)
    {
        if(CanJump == false) return;
        
        SetJumpForce(data);

        if (data.State == EInputState.End && _lastJumpForce.y >= 0f)
        {
            Jump();
        }
    }

    private async void SetPositionOnPlatform()
    {
        bool lookToRight = transform.position.x < 0f;
        float yEulerRot = lookToRight ? 90f : -90f;

        Vector3 nextRotation = new Vector3(0f, yEulerRot, 0f);
        Vector3 nextPos = transform.position;
        nextPos.z = 0f;
        
        if(Physics.Raycast(nextPos + Vector3.up * 0.5f, Vector3.down, out RaycastHit hit, 1.5f, _levelPlatformsLayerMask))
        {
            if (hit.collider.GetComponent<LevelPlatform>())
            {
                nextPos.y = hit.point.y;
            }
        }

        transform.DOMove(nextPos, SetStableOnPlatformDuration);
        transform.DORotate(nextRotation, SetStableOnPlatformDuration);

        await UniTask.WaitForSeconds(SetStableOnPlatformDuration);

        _isInJumpProcess = false;
    }

    private bool HavePlatformToLandOn()
    {
        if (Physics.Raycast(transform.position + Vector3.up * 0.5f, Vector3.down, out RaycastHit hit, 1.5f, _levelPlatformsLayerMask))
        {
            return hit.collider.GetComponent<LevelPlatform>();
        }

        return false;
    }
    
    public float GetLastNormalizedJumpForce01()
    {
        return _lastNormalizedJumpForce01;
    }

    private void SetJumpForce(InputData data)
    {
        float inputDeltaMagnitude = GetInputDeltaMagnitudeInViewportSpace(data);
        inputDeltaMagnitude = Mathf.Clamp(inputDeltaMagnitude, 0f, _maxInputDeltaInViewportSpace);

        _lastNormalizedJumpForce01 = Mathf.InverseLerp(0f, _maxInputDeltaInViewportSpace, inputDeltaMagnitude);
        _lastJumpForce = data.Direction * (_lastNormalizedJumpForce01 * _jumpForce);
    }

    private void Jump()
    {
        Vector3 currentPos = transform.position;
        currentPos.z = 0f;
        transform.position = currentPos;
        
        _lastJumpTime = Time.time;
        _rigidbody.isKinematic = false;
        _rigidbody.AddForce(_lastJumpForce, ForceMode.Impulse);
        
        bool jumpToRight = _lastJumpForce.x > 0f;
        float yEulerRot = jumpToRight ? 90f : -90f;
        
        transform.DORotate(new Vector3(0f, yEulerRot, 0f), JumpRotationDuration);
        
        _isInJumpProcess = true;
    }

    private float GetInputDeltaMagnitudeInViewportSpace(InputData data)
    {
        Vector2 inputDelta = _camera.ScreenToViewportPoint(data.EndPoint) - 
                             _camera.ScreenToViewportPoint(data.StartPoint);
        return inputDelta.magnitude;        
    }
}
