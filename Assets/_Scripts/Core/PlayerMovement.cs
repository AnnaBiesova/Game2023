using System;
using _Scripts;
using _Scripts.Core.Input;
using _Scripts.Patterns;
using _Scripts.Patterns.Events;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerMovement : Singleton<PlayerMovement>
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private float _jumpForce = 50f;
    [SerializeField] private float _maxInputDeltaInViewportSpace = 0.25f;

    private Camera _camera;
    
    private Vector2 _lastJumpForce;
    private float _lastNormalizedJumpForce01;
    
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
        if (other.gameObject.GetComponent<LevelPlatform>())
        {
            //_rigidbody.isKinematic = true;
        }
    }

    public float GetLastNormalizedJumpForce01()
    {
        return _lastNormalizedJumpForce01;
    }
    
    private void HandleInput(InputData data)
    {
        float inputDeltaMagnitude = GetInputDeltaMagnitudeInViewportSpace(data);
        inputDeltaMagnitude = Mathf.Clamp(inputDeltaMagnitude, 0f, _maxInputDeltaInViewportSpace);

        _lastNormalizedJumpForce01 = Mathf.InverseLerp(0f, _maxInputDeltaInViewportSpace, inputDeltaMagnitude);
        
        _lastJumpForce = data.Direction * (_lastNormalizedJumpForce01 * _jumpForce);

        if (data.State == EInputState.End && _lastJumpForce.y >= 0f)
        {
            _rigidbody.isKinematic = false;
            _rigidbody.AddForce(_lastJumpForce, ForceMode.Impulse);
        }
    }

    private float GetInputDeltaMagnitudeInViewportSpace(InputData data)
    {
        Vector2 inputDelta = _camera.ScreenToViewportPoint(data.EndPoint) - 
                             _camera.ScreenToViewportPoint(data.StartPoint);
        return inputDelta.magnitude;        
    }
}
