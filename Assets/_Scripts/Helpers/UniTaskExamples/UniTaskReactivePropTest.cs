using System.Threading;
using Cysharp.Threading.Tasks;
using Cysharp.Threading.Tasks.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Helpers.UniTaskExamples.ReactiveProperties
{
	public class UniTaskReactivePropTest : MonoBehaviour
	{
		[SerializeField] private Text textComponent;
		private AsyncReactiveProperty<int> rp;

		private void Awake()
		{
			Test();
		}

		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space)) rp.Value++;
		}

		private async void Test()
		{
			rp = new AsyncReactiveProperty<int>(99);

			// AsyncReactiveProperty itself is IUniTaskAsyncEnumerable, you can query by LINQ
			//rp.ForEachAsync(x => { Debug.Log(x); }, this.GetCancellationTokenOnDestroy());
			rp.WithoutCurrent().ForEachAsync(x => { Debug.Log(x); }, this.GetCancellationTokenOnDestroy());
            
			rp.Value = 10; // push 10 to all subscriber
			rp.Value = 11; // push 11 to all subscriber

			// WithoutCurrent ignore initial value
			// BindTo bind stream value to unity components.
			rp.WithoutCurrent().BindTo(this.textComponent);

			//await rp.WaitAsync(); // wait until next value set

			// also exists ToReadOnlyAsyncReactiveProperty
			var rp2 = new AsyncReactiveProperty<int>(99);
			var rorp = rp.CombineLatest(rp2, (x, y) => (x, y)).ToReadOnlyAsyncReactiveProperty(CancellationToken.None);
		}

		private async UniTaskVoid WaitForAsync(
			IReadOnlyAsyncReactiveProperty<string> asyncReadOnlyReactiveProperty,
			CancellationToken token)
		{
			await asyncReadOnlyReactiveProperty
				// 1秒待ってから MoveNextAsync() を実行する
				.ForEachAwaitWithCancellationAsync(async (x, ct) =>
				{
					Debug.Log(x);
					await UniTask.Delay(1000, cancellationToken: ct);
				}, token);
		}
	}
}