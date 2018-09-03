using Playmode.Ennemy.Strategies;
using Playmode.Entity.Senses;
using UnityEngine;

namespace Playmode.Pickable
{
    public delegate void PickUpEventHandler(PickableController ennemyController);

    public class PickableController : MonoBehaviour
    {
        protected EnnemySensor enemySensor;
        protected EnnemySensorEventHandler pickUpFunction;

        public event PickUpEventHandler OnPickUp;

        private void Awake()
        {
            enemySensor = GetComponent<EnnemySensor>();
            enemySensor.OnEnnemySeen += EnemySensor_OnEnnemySeen;
        }

        private void OnDisable()
        {
            enemySensor.OnEnnemySeen -= EnemySensor_OnEnnemySeen;
        }

        private void EnemySensor_OnEnnemySeen(Ennemy.EnnemyController ennemy)
        {
            if (!(ennemy.GetStrategyType() is ZombieStrategy))
            {
                pickUpFunction?.Invoke(ennemy);
                NotifyOnPickUp();
                Destroy(this.gameObject);
            }
        }

        private void NotifyOnPickUp()
        {
            OnPickUp?.Invoke(this);
        }
    }
}

