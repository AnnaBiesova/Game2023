﻿using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Scripts.Helpers.UniTaskExamples.ReactiveProperties
{
	public class UniTaskWaitUntilValueChangedSample : MonoBehaviour
	{
		private void Start()
		{
			CheckPositionAsync(this.GetCancellationTokenOnDestroy()).Forget();
		}

		private async UniTaskVoid CheckPositionAsync(CancellationToken token)
		{
			// 移動したら削除する
			await UniTask.WaitUntilValueChanged(
				target: transform,
				monitorFunction: x => x.position,
				cancellationToken: token);

			Destroy(gameObject);
		}

	}
}