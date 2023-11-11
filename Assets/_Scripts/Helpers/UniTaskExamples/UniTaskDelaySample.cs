using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Scripts.Helpers.UniTaskExamples.ReactiveProperties
{
	public class UniTaskDelaySample : MonoBehaviour
	{
		/// <summary>
		/// 無敵状態を表すフラグ
		/// </summary>
		private bool _isInvincible;
		
		
		private void Start()
		{
			var token = this.GetCancellationTokenOnDestroy();
			DelayDestroy(1000, token).Forget();

			var token1 = this.GetCancellationTokenOnDestroy();
			ChangeInvincibleAsync(token1).Forget();
		}

		/// <summary>
		/// 指定秒数後にGameObjectを破棄する
		/// </summary>
		private async UniTaskVoid DelayDestroy(int millSeconds, CancellationToken token)
		{
			await UniTask.Delay(millSeconds, cancellationToken: token);
			Destroy(gameObject);
		}

		/// <summary>
		/// 数フレームだけ無敵フラグを有効にする
		/// </summary>
		private async UniTaskVoid ChangeInvincibleAsync(CancellationToken token)
		{
			_isInvincible = true;
			await UniTask.DelayFrame(3, PlayerLoopTiming.Update, token);
			_isInvincible = false;
		}

	}
}