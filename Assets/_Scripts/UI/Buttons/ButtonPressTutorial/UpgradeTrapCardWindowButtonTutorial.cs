using UnityEngine;

namespace _Scripts.UI
{
	public class UpgradeTrapCardWindowButtonTutorial : ButtonPressTutorial
	{
		[SerializeField] private Animator buttonAnimator;

		protected override void OnEnable()
		{
			//buttonAnimator.enabled = TrapCardsSOManager.Instance.PlayerHaveMoneyForAnyTrapCardUpgrade();

			base.OnEnable();
		}
	}
}