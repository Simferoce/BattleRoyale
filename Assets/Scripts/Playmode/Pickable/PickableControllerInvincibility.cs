using UnityEngine;

namespace Playmode.Pickable
{
	public class PickableControllerInvincibility : PickableController
	{
		private int invincibilityTime = 15;
		
		public PickableControllerInvincibility() 
		{
			this.pickUpFunction = PickUpFunction;
		}

		public void PickUpFunction(Ennemy.EnnemyController ennemy)
		{
			ennemy.ActivateInvincibility(invincibilityTime);
		}
	}
}
