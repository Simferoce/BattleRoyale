using Playmode.Ennemy;
using UnityEngine;

namespace Playmode.Weapon
{
    public class Uzi : WeaponController
    {
        [SerializeField] private int upgradeDamage = 2;
        [SerializeField] private float upgradeShotFireRate = 0.05f;

        public override void Shoot(EnnemyController ennemyController)
        {
            base.Shoot(ennemyController);
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