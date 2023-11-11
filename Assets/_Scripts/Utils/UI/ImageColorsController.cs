using System;
using JoshH.UI;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Utils.UI
{
	public class ImageColorsController : MonoBehaviour
	{
		[SerializeField] private Image[] imagesToControl;
		[SerializeField] private UIGradient[] gradientsToControl;
		[SerializeField] private Color disabledColor;

		private Color[] defaultColors;
		private (Color, Color)[] defaultGradientsColors;

		private bool lastState = false;

		private bool inited;

		private void OnEnable()
		{
			SetState(lastState);
		}

		public void InitComponents()
		{
			imagesToControl = GetComponents<Image>();
			gradientsToControl = GetComponents<UIGradient>();
		}

		public void SetState(bool state)
		{
			lastState = state;

			if (inited == false)
			{
				inited = true;

				defaultColors = new Color[imagesToControl.Length];
				defaultGradientsColors = new (Color, Color)[gradientsToControl.Length];

				for (int i = 0; i < imagesToControl.Length; i++)
				{
					defaultColors[i] = imagesToControl[i].color;
				}

				for (int i = 0; i < gradientsToControl.Length; i++)
				{
					defaultGradientsColors[i] =
						(gradientsToControl[i].LinearColor1, gradientsToControl[i].LinearColor2);
				}
			}
			
			if (state == true)
			{
				for (int i = 0; i < imagesToControl.Length; i++)
				{
					imagesToControl[i].color = defaultColors[i];
				}

				for (int i = 0; i < gradientsToControl.Length; i++)
				{
					gradientsToControl[i].LinearColor1 = defaultGradientsColors[i].Item1;
					gradientsToControl[i].LinearColor2 = defaultGradientsColors[i].Item2;
				}
			}
			else
			{
				for (int i = 0; i < imagesToControl.Length; i++)
				{
					imagesToControl[i].color = disabledColor;
				}

				for (int i = 0; i < gradientsToControl.Length; i++)
				{
					gradientsToControl[i].LinearColor1 = disabledColor;
					gradientsToControl[i].LinearColor2 = disabledColor;
				}
			}
		}
	}
}