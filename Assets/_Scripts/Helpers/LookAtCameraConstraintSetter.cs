using _Scripts.Patterns.EntitiesManager;
using UnityEngine;
using UnityEngine.Animations;

namespace _Scripts.Helpers
{
	public class LookAtCameraConstraintSetter : MonoBehaviour
	{
		[SerializeField] protected LookAtConstraint lookAt;

		protected Transform cameraTransform;

		private Vector3 cameraPos;
        
		private void Start()
		{
			cameraTransform = EntitiesManager.GetAs<Transform>(EntityID.CAMERA_MAIN);
			
			SetConstraint();
		}

		protected virtual void SetConstraint()
		{
			lookAt.AddSource(new ConstraintSource()
			{
				sourceTransform = cameraTransform,
				weight = 1f,
			});

			lookAt.useUpObject = false;
			lookAt.constraintActive = true;
		}
	}
}