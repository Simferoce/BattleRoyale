using System;
using System.Collections.Generic;
using System.Linq;
using Assets.Scripts.Playmode.Entity.Senses;
using Playmode.Ennemy.BodyParts;
using Playmode.Entity.Senses;
using Playmode.Entity.Status;
using Playmode.Movement;
using Playmode.Pickable;
using UnityEngine;

namespace Playmode.Ennemy.Strategies
{
    public class Camper : IEnnemyStrategy
    {
        private readonly Mover mover;
        private readonly HandController handController;
        private readonly EnnemySensor enemySensor;
        private readonly PickableSensor pickableSensor;
        private readonly Health health;
        private PickableControllerMedKit pickableMedkit;
        private Vector2? randomSearch = null;
        private float sensibilityProximity = 0.5f;
        private float safeDistance = 5.0f;
        private float medDistance = 1.0f;
        private float height => 2f * Camera.main.orthographicSize;
        private float width => height * Camera.main.aspect;


        public Camper(Mover mover, HandController handController, EnnemySensor enemySensor, PickableSensor pickableSensor, Health health)
        {
            this.mover = mover;
            this.handController = handController;
            this.health = health;

            this.enemySensor = enemySensor;
            this.pickableSensor = pickableSensor;
        }

        public void Act()
        {
            PickableControllerMedKit medkit = TargetMethod.TargetMedkit(pickableSensor);
            
            if (pickableMedkit == null && medkit != null)
            {
                pickableMedkit = medkit;
                pickableMedkit.OnPickUp += OnPickUp;
            }

            if (pickableMedkit != null)
            {
                if (Vector2.Distance((Vector2)pickableMedkit.transform.position,mover.transform.position)<medDistance)
                {
                    if (health.HealthPoints>30)
                    {
                        Vector3? targetEnemy = TargetMethod.TargetEnemy(enemySensor);
                        if (targetEnemy == null)
                        {                           
                            mover.Rotate(-1);
                        }
                        else
                        {
                            mover.SetRotationToLookAt((Vector2)targetEnemy);
                            handController.Use();
                        } 
                    }
                    else
                    {                        
                        mover.MoveToward(pickableMedkit.transform.position);
                    }
                }
                else
                {
                    mover.MoveToward(pickableMedkit.transform.position);
                }
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

        private void OnPickUp(PickableController controller)
        {
            pickableMedkit.OnPickUp -= OnPickUp;
            pickableMedkit = null;
        }
    }
}