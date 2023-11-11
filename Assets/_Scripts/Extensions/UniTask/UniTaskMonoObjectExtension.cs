using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Cysharp.Jak.Extensions
{
	public static class UniTaskMonoObjectExtension
	{
		public static CancellationTokenSource GetDestroyAndManualLinkedTokenSource(this MonoBehaviour obj)
		{
			var cts = new CancellationTokenSource();
			var linkedCancellationTokenSource =
				CancellationTokenSource.CreateLinkedTokenSource(cts.Token,
					obj.GetCancellationTokenOnDestroy());

			return linkedCancellationTokenSource;
		}
	}
}