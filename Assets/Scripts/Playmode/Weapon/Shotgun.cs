using UnityEngine;

namespace Playmode.Weapon
{
	public class Shotgun : WeaponController
	{
		[SerializeField] private float angle;
		[SerializeField] private float nbBullets;
		private float angleEntreBalles => angle / (nbBullets - 1);
        
		public override void Shoot()
		{            
			if (CanShoot)
			{
				for (int i = 0; i < nbBullets; ++i)
				{
					GameObject bullet= Instantiate(bulletPrefab, transform.position, transform.rotation);
					bullet.transform.Rotate(new Vector3(0,0,angleEntreBalles*i-(angle/2)));
				}
				lastTimeShotInSeconds = Time.time;
			}
		}
	}
}