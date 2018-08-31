using Playmode.Ennemy;
using Playmode.Entity.Senses;
using System;
using UnityEngine;

namespace Playmode.Weapon
{
    public class WeaponController : MonoBehaviour
    {
        [Header("Behaviour")] [SerializeField] protected GameObject bulletPrefab;
        [SerializeField] protected float fireDelayInSeconds = 1f;
        protected int damageModifier = 0;

        protected float lastTimeShotInSeconds;

        protected bool CanShoot => Time.time - lastTimeShotInSeconds > fireDelayInSeconds;

        private void Awake()
        {
            ValidateSerialisedFields();
            InitializeComponent();
        }

        private void ValidateSerialisedFields()
        {
            if (fireDelayInSeconds < 0)
                throw new ArgumentException("FireRate can't be lower than 0.");
        }

        private void InitializeComponent()
        {
            lastTimeShotInSeconds = 0;
        }

        public virtual void Shoot(EnnemyController shooter)
        {           
            if (CanShoot)
            {
                GameObject bullet = Instantiate(bulletPrefab, transform.position, transform.rotation);
                HitStimulus hitStimulus = bullet.GetComponentInChildren<HitStimulus>();
                hitStimulus.HitPoints += damageModifier;
                hitStimulus.ShooterName = shooter.transform.root.name;
                lastTimeShotInSeconds = Time.time;
            }          
        }

        public virtual void UpdatePower(){}
    }
}