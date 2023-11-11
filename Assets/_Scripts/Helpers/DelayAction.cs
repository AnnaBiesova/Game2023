using System;
using System.Collections;
using _Scripts.Patterns;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace _Scripts.Helpers
{
    public static class DelayAction
    {
        public static async UniTaskVoid WaitForSeconds(Action action, float delay)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delay));
            action.Invoke();
        }

        public static async UniTaskVoid WaitForSecondsRealtime(Action action, float delay)
        {
            await UniTask.Delay(TimeSpan.FromSeconds(delay), true);
            action.Invoke();
        }

        public static async UniTaskVoid WaitWhile(Action action, Func<bool> waitFunc)
        {
            await UniTask.WaitWhile(waitFunc);
            action.Invoke();
        }

        public static async UniTaskVoid WaitUntil(Action action, Func<bool> waitFunc)
        {
            await UniTask.WaitUntil(waitFunc);
            action.Invoke();
        }
    }
}