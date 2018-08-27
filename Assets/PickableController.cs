using Playmode.Ennemy.BodyParts;
using Playmode.Entity.Senses;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Playmode.Pickable
{
    public class PickableController : MonoBehaviour
    {
        [SerializeField] GameObject hold;

        private EnnemySensor enemySensor;

        private void Awake()
        {
            enemySensor = GetComponent<EnnemySensor>();
            enemySensor.OnEnnemySeen += EnemySensor_OnEnnemySeen;
        }

        private void EnemySensor_OnEnnemySeen(Ennemy.EnnemyController ennemy)
        {
            ennemy.Take(Instantiate(hold));
            Destroy(this.gameObject);
        }
    }
}

