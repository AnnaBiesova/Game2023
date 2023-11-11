using System;
using UnityEngine;

namespace _Scripts.Utils.Camera
{
	public class CameraCopyFov : MonoBehaviour
	{
		[SerializeField] private UnityEngine.Camera copyFrom;
		[SerializeField] private UnityEngine.Camera copyTo;

		private void Update()
		{
			copyTo.fieldOfView = copyFrom.fieldOfView;
		}
	}
}