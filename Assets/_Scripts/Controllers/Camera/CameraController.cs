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
			vCam.Follow = PlayerMovement.Instance.transform;
			
			vCam.GetCinemachineComponent<CinemachineFramingTransposer>().m_TrackedObjectOffset = Vector3.zero;
		}

		private void LateUpdate()
		{
			if (follow) FollowTarget();

			Vector3 camPos = vCam.transform.position;
			camPos.x = 0f;
			vCam.transform.position = camPos;
		}

		private void FollowTarget()
		{
			Vector3 aimTargetPos = aimTarget.position;
			aimTargetPos = Vector3.SmoothDamp(aimTargetPos, nextAimTargetPos, ref cameraAimRefVelocity, 0.3f);
            
			aimTarget.position = aimTargetPos;
		}
	}
}