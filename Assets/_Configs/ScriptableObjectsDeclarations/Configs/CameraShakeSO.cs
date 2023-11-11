using UnityEngine;

namespace _Configs.ScriptableObjectsDeclarations.Configs
{
	[CreateAssetMenu(fileName = "CameraShakeSO", menuName = "Pawsome/Game Settings/Camera Shake Config", order = 0)]
	public class CameraShakeSO : ScriptableObject
	{
		[SerializeField] private float impulseDuration;
		[SerializeField] private Vector3 impulseCameraLocalVector;
		[SerializeField] private AnimationCurve impulseShape;

		public float ImpulseDuration => impulseDuration;
		public Vector3 ImpulseCameraLocalVector => impulseCameraLocalVector;
		public AnimationCurve ImpulseShape => impulseShape;
	}
}