using Cinemachine;
using UnityEngine;

namespace _Scripts.Utils.Camera.CinemachineExtensions
{
	[ExecuteInEditMode] [SaveDuringPlay] [AddComponentMenu("")] // Hide in menu
	public class LockCameraAxis : CinemachineExtension
	{
		[SerializeField] private bool lockX;
		[SerializeField] private bool lockY;
		[SerializeField] private bool lockZ;
		[Space]
		[SerializeField] private float xVal;
		[SerializeField] private float yVal;
		[SerializeField] private float zVal;

		protected override void PostPipelineStageCallback(
			CinemachineVirtualCameraBase vcam,
			CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
		{
			if (stage == CinemachineCore.Stage.Body)
			{
				var pos = state.RawPosition;

				if (lockX == true)
				{
					pos.x = xVal;
				}

				if (lockY == true)
				{
					pos.y = yVal;
				}

				if (lockZ == true)
				{
					pos.z = zVal;
				}
				
				state.RawPosition = pos;
			}
		}
	}
}