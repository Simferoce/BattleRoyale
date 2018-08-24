using System;
using System.Collections.Generic;
using System.Linq;
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
        private float sensibilityProximity = 0.5f;
        private float safeDistance = 5.0f;
        private float height => 2f * Camera.main.orthographicSize;
        private float width => height * Camera.main.aspect;


        public Normal(Mover mover, HandController handController, EnnemySensor enemySensor)
        {
            this.mover = mover;
            this.handController = handController;

            this.enemySensor = enemySensor;
        }

        public void Act()
        {
            Vector3? target;
            target = TargetEnemy();

            if(target != null)
            {
                mover.Follow((Vector3)target, 5f);
                
            } else
            {
                if (randomSearch == null)
                    Search();
                else if ((randomSearch - mover.transform.position).Value.magnitude - safeDistance < sensibilityProximity)
                    Search();
                mover.Follow((Vector2)randomSearch, safeDistance);
            }
        }

        private Vector3? TargetEnemy()
        {
            if(enemySensor.EnnemiesInSight.Count() > 0)
                return enemySensor.EnnemiesInSight.First().transform.position;
            else
                return null;
        }

        private void Search()
        {
            randomSearch = new Vector2(UnityEngine.Random.Range(-width / 2, width/2),
                UnityEngine.Random.Range(-height / 2, height/2));
        }
    }
}
