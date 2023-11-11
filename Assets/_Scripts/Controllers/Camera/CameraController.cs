using System;
using _Scripts.Patterns;
using _Scripts.Utils.Camera;
using Cinemachine;
using UnityEngine;

namespace _Scripts.Core
{
	public class CameraController : Singleton<CameraController>
	{
		[SerializeField] private CinemachineVirtualCamera vCam;
		[SerializeField] private Transform aimTarget;
		
		private bool follow = false;
		private bool levelStarted;

		private Vector3 cameraAimRefVelocity;
		private Vector3 nextAimTargetPos;
		private Transform target;

		private void Awake()
		{
			VirtualCameraChanger.OnCameraChanged += OnVirtualCameraChanged;

			vCam.Follow = PlayerMovement.Instance.transform;
			
			vCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset = Vector3.zero;
		}

		private void OnDestroy()
		{
			VirtualCameraChanger.OnCameraChanged -= OnVirtualCameraChanged;
		}
		
		private void LateUpdate()
		{
			if (follow) FollowTarget();

			Vector3 camPos = vCam.transform.position;
			camPos.x = 0f;
			vCam.transform.position = camPos;
		}

		private void OnVirtualCameraChanged(CinemachineVirtualCamera virtualCamera)
		{
			vCam.CopyComponentSettings<CinemachineTransposer>(virtualCamera);
			vCam.CopyComponentSettings<CinemachineComposer>(virtualCamera);
			
			cameraAimRefVelocity = Vector3.zero;
			
			vCam = virtualCamera;
			vCam.LookAt = aimTarget;
		}

		
		public void SetAimTargetPos(Vector3 aimTargetPos, bool instant)
		{
			nextAimTargetPos = aimTargetPos;
			
			if (instant)
			{
				aimTarget.position = nextAimTargetPos;
				vCam.transform.rotation = Quaternion.LookRotation(nextAimTargetPos - vCam.transform.position, Vector3.up);
			}
		}

		private void FollowTarget()
		{
			Vector3 aimTargetPos = aimTarget.position;
			aimTargetPos = Vector3.SmoothDamp(aimTargetPos, nextAimTargetPos, ref cameraAimRefVelocity, 0.3f);
            
			aimTarget.position = aimTargetPos;
		}
	}
}