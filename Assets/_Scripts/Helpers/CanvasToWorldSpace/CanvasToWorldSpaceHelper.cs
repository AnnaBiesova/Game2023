using System;
using UnityEngine;

namespace _Scripts.Helpers.CanvasToWorldSpace
{
	[Serializable]
	public class CanvasToWorldSpaceHelper
	{
		[SerializeField] private Transform targetObjectOnCanvas;
		[SerializeField] private Camera mainCamera;
		[SerializeField] private float distanceFromCamera;
        
		public CanvasToWorldSpaceHelper(Transform targetObjectOnCanvas, Camera mainCamera, float distanceFromCamera)
		{
			this.targetObjectOnCanvas = targetObjectOnCanvas;
			this.mainCamera = mainCamera;
			this.distanceFromCamera = distanceFromCamera;
		}

		public Vector3 ConvertPositionOnCanvasToWorldPosition()
		{
			Vector3 canvasTargetPos = targetObjectOnCanvas.position;
			canvasTargetPos.z = distanceFromCamera;

			Vector3 worldPos = mainCamera.ScreenToWorldPoint(canvasTargetPos);
			
			return worldPos;
		}
	}
}