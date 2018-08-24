using UnityEngine;

namespace Playmode.Weapon
{
    public abstract class Weapon
    {
        public abstract void Shoot(Vector3 position, Quaternion rotation, GameObject bullet);
        public abstract float GetFireDelayInSeconds();
        public abstract int GetDamageWeapon();

        public static void ShootInCone(float angle,int nbBullets, Vector3 position, Quaternion rotation, WeaponController controller)
        {
            GameObject[] bullets= new GameObject[nbBullets];
            for (int i = 0; i < nbBullets; ++i)
            {
                
            }
        }
    }
}