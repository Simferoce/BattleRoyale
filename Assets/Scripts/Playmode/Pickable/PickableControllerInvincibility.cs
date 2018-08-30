using UnityEngine;

namespace Playmode.Pickable
{
	public class PickableControllerInvincibility : PickableController
	{

		public PickableControllerInvincibility() 
		{
			this.pickUpFunction = PickUpFunction;
		}

		public void PickUpFunction(Ennemy.EnnemyController ennemy)
		{
			ennemy.ActivateInvincibility(15);
		}
	}
}
