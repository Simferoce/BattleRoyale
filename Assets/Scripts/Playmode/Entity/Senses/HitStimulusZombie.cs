using Playmode.Ennemy;
using Playmode.Ennemy.Strategies;
using System;
using UnityEngine;

namespace Playmode.Entity.Senses
{
    public class HitStimulusZombie : HitStimulus
    {
        [Header("Behaviour")] [SerializeField] private int hitPoints = 100;

        private void Awake()
        {
            ValidateSerializeFields();
        }

        private void ValidateSerializeFields()
        {
            if (hitPoints < 0)
                throw new ArgumentException("Hit points can't be less than 0.");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            HitSensor hitSensor = other.GetComponent<Entity.Senses.HitSensor>();
            if(hitSensor != null && !(hitSensor.transform.parent.GetComponentInChildren<EnnemyController>().GetStrategyType() is ZombieStrategy))
            {
                hitSensor.Hit(hitPoints, ShooterName);
            }
        }
    }
}