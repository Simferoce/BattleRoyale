﻿using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Playmode.Entity.Senses;
using Playmode.Ennemy.BodyParts;
using Playmode.Entity.Senses;
using Playmode.Movement;
using Playmode.Pickable;
using Playmode.Util.Values;
using UnityEngine;

namespace Playmode.Ennemy.Strategies
{
    class Normal : IEnnemyStrategy
    {
        private readonly Mover mover;
        private readonly HandController handController;
        private readonly EnnemySensor enemySensor;
        private readonly PickableSensor pickableSensor;
        private Vector2? randomSearch = null;
        private float sensibilityProximity = 0.5f;
        private float safeDistance = 5.0f;


        public Normal(Mover mover, HandController handController, EnnemySensor enemySensor)
        {
            this.mover = mover;
            this.handController = handController;

            this.enemySensor = enemySensor;
        }

        public void Act()
        {
            Vector3? target;
            target = TargetMethod.TargetEnemy(enemySensor);

            if(target != null)
            {
                mover.MoveToward((Vector2)target);
                handController.Use();
            } else
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
