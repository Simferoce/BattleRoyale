using UnityEngine;

namespace Playmode.Pickable
{
    public class PickableControllerWeapon : PickableController
    {
        public PickableControllerWeapon()
        {
            this.pickUpFunction = PickUpFunction;
        }

        public void PickUpFunction(Ennemy.EnnemyController ennemy)
        {
            ennemy.Take(this.transform.parent.gameObject);
        }
    }
}

