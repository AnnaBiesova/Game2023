using UnityEngine;

namespace _Scripts.Extensions
{
	public static class AnimatorExtensions
	{
		public static bool IsPlaying(this Animator animator)
		{
			return animator.GetCurrentAnimatorStateInfo(0).length >
			       animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
		}
	}
}