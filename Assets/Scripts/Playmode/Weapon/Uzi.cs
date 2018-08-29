using Boo.Lang.Environments;
using UnityEngine;

namespace Playmode.Weapon
{
    public class Uzi : WeaponController
    {
        [SerializeField] private int upgradeDamage = 2;

        public override void Shoot()
        {
            base.Shoot();
        }

        public override void UpdatePower()
        {
            damageModifier += upgradeDamage;
        }
    }
}