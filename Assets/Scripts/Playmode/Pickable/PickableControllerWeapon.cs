using UnityEngine;

namespace Playmode.Pickable
{
    public class PickableControllerWeapon : PickableController
    {
        [SerializeField] GameObject hold;

        public PickableControllerWeapon()
        {
            this.pickUpFunction = PickUpFunction;
        }

        public void PickUpFunction(Ennemy.EnnemyController ennemy)
        {
            ennemy.Take(Instantiate(hold));
        }
    }
}

