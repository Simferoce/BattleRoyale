using Playmode.Entity.Senses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playmode.Pickable
{
    public class PickableControllerMedKit : PickableController
    {
        [SerializeField] private int healPoints;

        private EnnemySensor enemySensor;

        private void Awake()
        {
            enemySensor = GetComponent<EnnemySensor>();
            enemySensor.OnEnnemySeen += EnemySensor_OnEnnemySeen;
        }

        private void EnemySensor_OnEnnemySeen(Ennemy.EnnemyController ennemy)
        {
            ennemy.Heal(healPoints);
            Destroy(this.gameObject);
        }
        
    }
}

