using UnityEngine;

namespace Playmode.Pickable
{
    public class PickableControllerMedKit : PickableController
    {
        [SerializeField] private int healPoints;

        public PickableControllerMedKit() 
        {
            this.pickUpFunction = PickUpFunction;
        }

        public void PickUpFunction(Ennemy.EnnemyController ennemy)
        {
            ennemy.Heal(healPoints);
        }
    }
}

