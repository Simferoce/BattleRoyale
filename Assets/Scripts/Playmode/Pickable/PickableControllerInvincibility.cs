using UnityEngine;

namespace Playmode.Pickable
{
	public class PickableControllerInvincibility : PickableController
	{
		private const int INVINCIBILITY_TIME = 15;
		
		public PickableControllerInvincibility() 
		{
			this.pickUpFunction = PickUpFunction;
		}

		public void PickUpFunction(Ennemy.EnnemyController ennemy)
		{
			ennemy.ActivateInvincibility(INVINCIBILITY_TIME);
		}
	}
}
