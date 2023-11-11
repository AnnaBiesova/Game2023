using Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

public sealed class CameraFOVSetter : MonoBehaviour
{
	private CinemachineVirtualCamera m_camera;
	private Camera _camera;

	private bool cinemachine;

	private bool cameraAjustedInFirstUpdate;

	[SerializeField] private float m_fieldOfView = 60f;
	[SerializeField] private float aspectInEditor = 0.5625f;

	[SerializeField] private float orthoWidthSize;

	private void Awake()
	{
		m_camera = GetComponent<CinemachineVirtualCamera>();
		_camera = GetComponent<Camera>();

		cinemachine = m_camera != null && _camera == null;
	}
	
	private void Update()
	{
		bool isOrtho = (m_camera != null && m_camera.m_Lens.Orthographic || _camera != null && _camera.orthographic);
		
		if (isOrtho)
		{
			if (Screen.width < Screen.height)
			{
				float newOtrhoSize = orthoWidthSize / 2 * Screen.height / Screen.width;

				if (cinemachine)
				{
					m_camera.m_Lens.OrthographicSize = newOtrhoSize;
				}
				else
				{
					_camera.orthographicSize = newOtrhoSize;
				}
			}
		}
		else
		{
			float currentFov = cinemachine ? m_camera.m_Lens.FieldOfView : _camera.fieldOfView;
			float aspect = (Screen.width / (float)Screen.height);

			currentFov = Camera.VerticalToHorizontalFieldOfView(currentFov, aspect);

			float fovDifference = Mathf.Abs(m_fieldOfView - currentFov);

			if (fovDifference > 1f && aspect <= aspectInEditor)
			{
				RefreshCamera(aspect);
			}
		}
	}

	public void RefreshCamera(float aspect)
	{
		m_camera = GetComponent<CinemachineVirtualCamera>();
		_camera = GetComponent<Camera>();

		AdjustCamera(aspect);
	}

	private void AdjustCamera(float aspect)
	{
		float _1OverAspect = 1f / aspect;
		float fov = 2f * Mathf.Atan(_1OverAspect * Mathf.Tan(m_fieldOfView * Mathf.Deg2Rad * 0.5f)) *
		            Mathf.Rad2Deg;
		if (m_camera != null)
		{
			m_camera.m_Lens.FieldOfView = fov;
		}

		if (_camera != null)
		{
			_camera.fieldOfView = fov;
		}
	}
}