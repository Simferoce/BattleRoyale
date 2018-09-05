using Playmode.Ennemy.BodyParts;
using Playmode.Entity.Senses;
using Playmode.Movement;
using UnityEngine;

namespace Playmode.Ennemy.Strategies
{
    class Normal : IEnnemyStrategy
    {
        private readonly Mover mover;
        private readonly HandController handController;
        private readonly EnnemySensor enemySensor;
        private Vector2? randomSearch = null;
        private readonly float distanceFollow = 1f;


        public Normal(Mover mover, HandController handController, EnnemySensor enemySensor)
        {
            this.mover = mover;
            this.handController = handController;
            this.enemySensor = enemySensor;
        }

        public void Act()
        {
            EnnemyController target = TargetMethod.TargetEnemy(enemySensor);

            if(target != null)
            {
                mover.Follow(target.transform.position, distanceFollow);
                handController.Use();
            }
            else
            {
                TargetMethod.SearchEnemyOrPickable(mover, ref randomSearch);
            }
        }
    }
}
