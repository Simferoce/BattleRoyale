using Playmode.Util.Values;
using System;
using UnityEngine;
using Playmode.Event;
using Playmode.Ennemy;

namespace Playmode.Entity.Status
{
    public delegate void HealthEventHandler();

    public class Health : MonoBehaviour
    {
        [SerializeField] private int healthPoints = 100;

        private EventHandlerEnemyDeath enemyDeathChannel;
        private EnnemyController ennemyController;

        public event HealthEventHandler OnDeath;

        public int HealthPoints
        {
            get { return healthPoints; }
            private set
            {
                healthPoints = value < 0 ? 0 : value;

                if (healthPoints <= 0) NotifyDeath();
            }
        }

        private void Awake()
        {
            ValidateSerialisedFields();

            enemyDeathChannel = GameObject.FindWithTag(Tags.MainController).GetComponent<EventHandlerEnemyDeath>();
            ennemyController = GetComponent<EnnemyController>();
        }

        private void ValidateSerialisedFields()
        {
            if (healthPoints < 0)
                throw new ArgumentException("HealthPoints can't be lower than 0.");
        }

        public void Hit(int hitPoints)
        {
            HealthPoints -= hitPoints;
        }

        public void Heal(int healthPoints)
        {
            HealthPoints += healthPoints;
        }

        private void NotifyDeath()
        {
            enemyDeathChannel.Publish(new EnemyDeathData(ennemyController.Name));
            OnDeath?.Invoke();
        }
    }
}