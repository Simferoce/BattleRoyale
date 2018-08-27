using UnityEngine;

namespace Playmode.Weapon
{
    public class Shotgun:WeaponController
    {
        static float angle=60;
        static float nbBullets = 5;
        readonly float angleEntreBalles = angle / (nbBullets - 1); 
        
        public override void Shoot()
        {            
            Debug.Log("test");
            for (int i = 0; i < nbBullets; ++i)
            {
                GameObject bullet= Instantiate(bulletPrefab, transform.position, transform.rotation);
                bullet.transform.Rotate(new Vector3(0,0,angleEntreBalles*i-(angle/2)));
            }           
        }
    }
}