using Playmode.Entity.Senses;
using Playmode.Movement;
using UnityEngine;

namespace Playmode.Ennemy.Strategies
{
    class ZombieStrategy : IEnnemyStrategy
    {
        private readonly EnnemySensor enemySensor;
        private readonly Mover mover;
        private Vector2? randomSearch = null;


        public ZombieStrategy(Mover mover, EnnemySensor enemySensor)
        {
            this.enemySensor = enemySensor;
            this.mover = mover;
        }

        public void Act()
        {
            EnnemyController target = TargetMethod.TargetEnemyNotZombie(enemySensor);

            if (target != null)
            {
                mover.MoveToward(target.transform.position);
            }
            else
            {
                TargetMethod.SearchEnemyOrPickable(mover, ref randomSearch);
            }
        }
    }
}
