using Boo.Lang.Environments;
using UnityEngine;

namespace Playmode.Weapon
{
    public class Uzi : WeaponController
    {
        [SerializeField] private int upgradeDamage = 2;
        [SerializeField] private float upgradeShotFireRate = 0.05f;

        public override void Shoot()
        {
            base.Shoot();
        }

        public override void UpdatePower()
        {
            damageModifier += upgradeDamage;
            if(fireDelayInSeconds - upgradeShotFireRate >= upgradeShotFireRate)
            {
                fireDelayInSeconds -= upgradeShotFireRate;
            } 
            else
            {
                fireDelayInSeconds = upgradeShotFireRate;
            }
        }
    }
}