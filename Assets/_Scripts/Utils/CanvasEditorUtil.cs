using System.Linq;
using _Scripts.Extensions;
using JoshH.UI;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Scripts.Utils.CanvasEditorUtil
{
	public class CanvasEditorUtil : MonoBehaviour
	{

		private void SetRaycastTargets()
		{
			int disableCounter = 0;
			int enableCounter = 0;
			
			ProcessChilds(transform, false);

			Debug.LogWarning($"Disabled raycast targets count: {disableCounter}");
			Debug.LogWarning($"Enabled raycast targets count: {enableCounter}");
			
			void ProcessChilds(Transform parent, bool raycastStayOnChildren)
			{
				for (int i = 0; i < parent.childCount; i++)
				{
					Transform child = parent.GetChild(i);

					if (raycastStayOnChildren == false)
					{
						raycastStayOnChildren = child.GetComponents<Component>()
							.Any(c => c is IEventSystemHandler || c is CanvasGroup { blocksRaycasts: true });
					}
					
					Graphic[] raycastComponents = child.GetComponents<Graphic>();

					foreach (Graphic raycastComponent in raycastComponents)
					{
						raycastComponent.raycastTarget = raycastStayOnChildren;

						if (raycastStayOnChildren) enableCounter++;
						else disableCounter++;
					}
					
					ProcessChilds(child, raycastStayOnChildren);
				}
			}
		}


		public void SetAnimatorsTimeMode(AnimatorUpdateMode updateMode)
		{
			int animatorsCount = 0;
			
			ProcessChilds(transform);
			
			Debug.LogWarning($"Update mode: {updateMode} seted on total: {animatorsCount} animators");

			void ProcessChilds(Transform parent)
			{
				for (int i = 0; i < parent.childCount; i++)
				{
					Transform child = parent.GetChild(i);
					
					Animator animator = child.GetComponent<Animator>();
					
					if (animator != null)
					{
						animator.updateMode = updateMode;
						animatorsCount++;
					}

					ProcessChilds(child);
				}
			}
		}


		private void AddCanvasRenderers()
		{
			foreach (var t1 in transform.GetComponentsInChildren<Transform>(true))		
			{
				if(t1.GetComponent<CanvasRenderer>()) continue;
				t1.gameObject.AddComponent<CanvasRenderer>();
			}
		}


		private void SetCullTransparentVerts(bool cull)
		{
			foreach (CanvasRenderer canvasRenderer in GetComponentsInChildren<CanvasRenderer>(true))
			{
				canvasRenderer.cullTransparentMesh = cull;
			}
		}


		private void SetTextsBestFit(bool bestFit)
		{
			foreach (Text text in GetComponentsInChildren<Text>(true))
			{
				text.resizeTextForBestFit = bestFit;
			}
		}


		private void SetTextsRich(bool rich)
		{
			foreach (Text text in GetComponentsInChildren<Text>(true))
			{
				text.supportRichText = rich;
			}
		}


		private void CopyContentsFromHierarchy(Transform fromParent, Transform toParent)
		{
			TransformExtensions.ProcessParallelHierarchies(fromParent, toParent, CopyCanvasComponentsProperties);
            
			void CopyCanvasComponentsProperties(Transform from, Transform to)
			{
				// Copy Text component properties if they exist
				Text fromText = from.GetComponent<Text>();
				Text toText = to.GetComponent<Text>();
				if (fromText != null && toText != null)
				{
					toText.text = fromText.text;
					toText.fontSize = fromText.fontSize;
				}

				// Copy Image component properties if they exist
				Image fromImage = from.GetComponent<Image>();
				Image toImage = to.GetComponent<Image>();
				if (fromImage != null && toImage != null)
				{
					toImage.sprite = fromImage.sprite;
					toImage.color = fromImage.color;
				}

				// Copy UIGradient properties if they exist
				// Assuming UIGradient is a custom script you have. Modify the field names if they're different.
				UIGradient fromGradient = from.GetComponent<UIGradient>();
				UIGradient toGradient = to.GetComponent<UIGradient>();
				if (fromGradient != null && toGradient != null)
				{
					toGradient.LinearColor1 = fromGradient.LinearColor1;
					toGradient.LinearColor2 = fromGradient.LinearColor2;
				}
			}
		}
	}
}