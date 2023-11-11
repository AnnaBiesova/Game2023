using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Helpers.UniTaskExamples
{
	public class _MyUniTaskSamples : MonoBehaviour
	{
		private void SubscribeToReactiveIntPropertyWithMethod()
		{
			AsyncReactiveProperty<int> reactiveProperty = new AsyncReactiveProperty<int>(123);
			reactiveProperty.BindTo(this, OnIntReactivePropertyUpdate);
			
			reactiveProperty.Value = 321;
		}

		private void OnIntReactivePropertyUpdate(_MyUniTaskSamples thisObj, int newVal)
		{
			Debug.LogWarning($"Object {thisObj.name} reacts to reactive property change. " +
			                 $"New value = {newVal}");
		}
	}
}