using _Scripts.Patterns;
using _Scripts.Patterns.Events;
using UnityEngine;

namespace _Scripts.Core.Input
{
	public sealed class TouchInputHandler : Singleton<TouchInputHandler>
	{
		[field: SerializeField] private InputData _inputData = default;
		private Camera mainCam;
		[field: SerializeField] public Vector2 StartPoint { get; private set; }
		[field: SerializeField] public Vector2 EndPoint { get; private set; }
		[field: SerializeField] public EInputState State { get; private set; }

		public bool HaveInputInThisFrame = false;

		private void Awake()
		{
			mainCam = Camera.main;
			UnityEngine.Input.multiTouchEnabled = false;
		}

		private void FixedUpdate()
		{
			if (UnityEngine.Input.GetMouseButton(0) && (State != EInputState.Start && State != EInputState.Continue))
			{
				State = EInputState.Start;
				StartPoint = UnityEngine.Input.mousePosition;
			}
			else if (UnityEngine.Input.GetMouseButton(0) && (State == EInputState.Start || State == EInputState.Continue))
			{
				State = EInputState.Continue;
				EndPoint = StartPoint != Vector2.zero
					? (Vector2)UnityEngine.Input.mousePosition
					: Vector2.zero;
			}

			if (UnityEngine.Input.GetMouseButton(0) == false && (State == EInputState.Start || State == EInputState.Continue))
			{
				State = EInputState.End;
				EndPoint = UnityEngine.Input.mousePosition;
			}
			
			FinalizeInputProcessing();
		}


		private void FinalizeInputProcessing()
		{
			HaveInputInThisFrame = State != EInputState.None;
			
			InputData data = _inputData.ProcessInput(this);

			if (State != EInputState.None)
			{
				this.OnEvent(EventID.INPUT, data);
			}

			if (State == EInputState.End || State == EInputState.None)
			{
				SoftReset();
				HardReset();
			}
		}

		private void HardReset()
		{
			StartPoint = Vector2.zero;
			EndPoint = Vector2.zero;
			State = EInputState.None;
		}

		public void SoftReset()
		{
			StartPoint = EndPoint;
		}
	}
}