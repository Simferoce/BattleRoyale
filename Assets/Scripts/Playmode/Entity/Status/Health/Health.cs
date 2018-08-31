using System;
using UnityEngine;

namespace Playmode.Entity.Status.Health
{
    public delegate void HealthChangeEventHandler(int newHealth);

    public class Health : MonoBehaviour
    {
        [SerializeField] private int healthPoints = 100;

        public event HealthChangeEventHandler OnHealthChange;

        public int HealthPoints
        {
            get { return healthPoints; }
            private set
            {
                int temp = healthPoints;
                
                healthPoints = value < 0 ? 0 : value;

                if (temp != value)
                    NotifyHealthChange();
            }
        }

        private void Awake()
        {
            ValidateSerialisedFields();
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

        private void NotifyHealthChange()
        {
            OnHealthChange?.Invoke(healthPoints);
        }
    }
}