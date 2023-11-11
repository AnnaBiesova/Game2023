using UnityEngine;

namespace _Scripts.Core.Input
{
	public struct InputData
	{
		public static InputData LastInputData = default;
		private static TouchInputHandler inputHandler;

		[field: SerializeField] public EInputState State { get; private set; }
		[field: SerializeField] public Vector2 StartPoint { get; private set; }
		[field: SerializeField] public Vector2 EndPoint { get; private set; }
		[field: SerializeField] public Vector2 Delta { get; private set; }
		[field: SerializeField] public Vector2 Direction { get; private set; }
		[field: SerializeField] public float Distance { get; private set; }

		public bool HaveInputInThisFrame;

		public InputData ProcessInput(TouchInputHandler handler)
		{
			inputHandler = handler;

			State = handler.State;
			StartPoint = handler.StartPoint;
			EndPoint = handler.EndPoint;

			Delta = EndPoint - StartPoint;
			Vector2 result = State == EInputState.Start ? Vector2.zero : handler.EndPoint - handler.StartPoint;
			Distance = result.magnitude;
			Direction = result == Vector2.zero ? Vector2.zero : result / Distance;

			HaveInputInThisFrame = handler.HaveInputInThisFrame;
			
			LastInputData = this;
			return LastInputData;
		}

		public void Reset()
		{
			if (inputHandler != null) inputHandler.SoftReset();
		}
	}
}