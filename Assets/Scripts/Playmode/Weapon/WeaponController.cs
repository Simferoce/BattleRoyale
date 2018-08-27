using System;
using UnityEngine;

namespace Playmode.Weapon
{
    public class WeaponController : MonoBehaviour
    {
        [Header("Behaviour")] [SerializeField] protected GameObject bulletPrefab;
        [SerializeField] private float fireDelayInSeconds = 1f;
        
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

        public virtual void Shoot()
        {           
            if (CanShoot)
            {
                Instantiate(bulletPrefab, transform.position, transform.rotation);
                lastTimeShotInSeconds = Time.time;
            }          
        }
    }
}