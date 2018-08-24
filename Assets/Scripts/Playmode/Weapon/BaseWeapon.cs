using UnityEngine;

namespace Playmode.Weapon
{
    public class BaseWeapon:Weapon
    {
        public override void Shoot(Vector3 position, Quaternion rotation, GameObject bullet)
        {
        }

        public override float GetFireDelayInSeconds()
        {
            return 0.0f;
        }

        public override int GetDamageWeapon()
        {
            return 0;
        }
    }
}