using Playmode.Ennemy;
using Playmode.Entity.Senses;
using UnityEngine;

namespace Playmode.Weapon
{
	public class Shotgun : WeaponController
	{
		[SerializeField] private float angle;
		[SerializeField] private float nbBullets;
        [SerializeField] private int upgradeDamage = 10;

        private float angleEntreBalles => angle / (nbBullets - 1);
        
		public override void Shoot(EnnemyController ennemyController)
		{
            if (CanShoot)
			{
                for (int i = 0; i < nbBullets; ++i)
				{
                    GameObject bullet= Instantiate(bulletPrefab, transform.position, transform.rotation);
                    HitStimulus hitStimulus = bullet.GetComponentInChildren<HitStimulus>();
                    hitStimulus.HitPoints += damageModifier;
                    hitStimulus.ShooterName = ennemyController.transform.root.name;
					bullet.transform.Rotate(new Vector3(0,0,angleEntreBalles*i-(angle/2)));
				}
				lastTimeShotInSeconds = Time.time;
			}
		}

        public override void UpdatePower()
        {
            damageModifier += upgradeDamage;
        }
    }
}