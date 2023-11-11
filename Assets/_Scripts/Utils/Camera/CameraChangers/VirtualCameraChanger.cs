using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Extensions;
using Cinemachine;
using UnityEngine;

namespace _Scripts.Utils.Camera
{
	public class VirtualCameraChanger : MonoBehaviour
	{
		public static Action<CinemachineVirtualCamera> OnCameraChanged;
		
		private static bool inited;
		private static List<CinemachineVirtualCamera> camerasOnLevel = new List<CinemachineVirtualCamera>();
		private static CinemachineBrain brain;

		[SerializeField] public CinemachineVirtualCamera camTo;

		[SerializeField] private TransformLocalStruct cameraDefaultLocalStruct;
		[SerializeField] private TransformGlobalStruct cameraDefaultGlobalStruct;

		public Vector3 StoredForwardVector =>
			Quaternion.Euler(cameraDefaultGlobalStruct.EulerAngles) * Vector3.forward; 
		
		protected virtual void Awake()
		{
			cameraDefaultLocalStruct = camTo.transform.ToLocalStruct();
			cameraDefaultGlobalStruct = camTo.transform.ToGlobalStruct();
			
			if (inited) return;
			inited = true;
			brain = FindObjectOfType<CinemachineBrain>();
			camerasOnLevel = FindObjectsOfType<CinemachineVirtualCamera>().ToList();
		}
		
		protected virtual void OnDestroy()
		{
			inited = false;
		}

		private void ResetCameraOrientationToDefault()
		{
			camTo.m_LookAt = null;
			camTo.transform.FromLocalStructToLocal(cameraDefaultLocalStruct);
		}

		protected void ChangeCameraWithBlend()
		{
			ChangeCamera(false);
		}
		
		public void ChangeCamera(bool instant)
		{
			CinemachineVirtualCamera camFrom = camerasOnLevel.Aggregate((x, y) => x.Priority >= y.Priority ? x : y);
			
			if (camFrom.Priority < camTo.Priority) return;

			ResetCameraOrientationToDefault();
			
			(camFrom.Priority, camTo.Priority) = (camTo.Priority, camFrom.Priority);
			OnCameraChanged?.Invoke(camTo);

			/*if (instant)
			{
				brain.CompleteCurrentBlend();
			}*/
		}
	}
}
