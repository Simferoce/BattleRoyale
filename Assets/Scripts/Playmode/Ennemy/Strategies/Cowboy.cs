using Assets.Scripts.Playmode.Entity.Senses;
using Playmode.Ennemy;
using Playmode.Ennemy.BodyParts;
using Playmode.Ennemy.Strategies;
using Playmode.Entity.Senses;
using Playmode.Movement;
using Playmode.Pickable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Playmode.Ennemy.Strategies
{
    public class Cowboy : IEnnemyStrategy
    {
        private readonly Mover mover;
        private readonly HandController handController;
        private readonly EnnemySensor enemySensor;
        private readonly PickableSensor pickableSensor;
        private Vector2? randomSearch = null;
        private float sensibilityProximity = 0.5f;
        private float safeDistance = 5.0f;


        public Cowboy(Mover mover, HandController handController, EnnemySensor enemySensor, PickableSensor pickableSensor)
        {
            this.mover = mover;
            this.handController = handController;

            this.enemySensor = enemySensor;
            this.pickableSensor = pickableSensor;
        }

        public void Act()
        {
            PickableController target = TargetMethod.TargetWeapon(pickableSensor);

            if (target != null)
            {
                mover.MoveToward((Vector2)target.transform.position);
                handController.Use();
            }
            else
            {
                if (randomSearch == null)
                    randomSearch = TargetMethod.Search();
                else if ((randomSearch - mover.transform.position).Value.magnitude - safeDistance < sensibilityProximity)
                    randomSearch = TargetMethod.Search();
                mover.MoveToward((Vector2)randomSearch);
            }
        }
    }
}
