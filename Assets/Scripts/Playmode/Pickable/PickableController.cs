using Playmode.Entity.Senses;
using System.Collections;
using System.Collections.Generic;
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
            pickUpFunction?.Invoke(ennemy);
            NotifyOnPickUp();
            Destroy(this.gameObject);
        }

        private void NotifyOnPickUp()
        {
            OnPickUp?.Invoke(this);
        }
    }
}

