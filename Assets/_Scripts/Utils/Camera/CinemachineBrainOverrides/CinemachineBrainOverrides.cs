namespace _Scripts.Utils.Camera.CinemachineBrainOverrides
{
	//Add methods from this class to local cinemachine package
	public class CinemachineBrainOverrides
	{
		/*public void CompleteCurrentBlend(bool completeDamp = true)
		{
			if (mFrameStack.Count == 0)
			{
				return;
			}

			// Complete current blend
			mFrameStack[0].blend.Duration = 0;
			ManualUpdate();
			// Use -1 to prevent dolly camera finds closest point
			var deltaTime = -1;
			CinemachineCore.UpdateFilter filter = CinemachineCore.UpdateFilter.Late;
			if (m_UpdateMethod == UpdateMethod.SmartUpdate)
			{
				// Track the targets
				UpdateTracker.OnUpdate(UpdateTracker.UpdateClock.Late);
				filter = CinemachineCore.UpdateFilter.SmartLate;
			}

			UpdateVirtualCameras(filter, deltaTime);
			// Choose the active vcam and apply it to the Unity camera
			if (m_BlendUpdateMethod != BrainUpdateMethod.FixedUpdate)
				ProcessActiveCamera(deltaTime);
			// Complete damp
			if (completeDamp && ActiveVirtualCamera is CinemachineVirtualCameraBase vcam)
			{
				vcam.PreviousStateIsValid = false;
				ManualUpdate();
			}

			mFrameStack.Clear();
		}*/

	}
}