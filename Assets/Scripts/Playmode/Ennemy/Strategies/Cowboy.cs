using Playmode.Ennemy.BodyParts;
using Playmode.Entity.Senses;
using Playmode.Movement;
using Playmode.Pickable;
using UnityEngine;

namespace Playmode.Ennemy.Strategies
{
    public class Cowboy : IEnnemyStrategy
    {
        private readonly Mover mover;
        private readonly HandController handController;
        private readonly EnnemySensor enemySensor;
        private readonly PickableSensor pickableSensor;
        private readonly float distanceFollow = 1f;
        private Vector2? randomSearch = null;


        public Cowboy(Mover mover, HandController handController, EnnemySensor enemySensor, PickableSensor pickableSensor)
        {
            this.mover = mover;
            this.handController = handController;

            this.enemySensor = enemySensor;
            this.pickableSensor = pickableSensor;
        }

        public void Act()
        {
            PickableControllerWeapon targetWeapon = TargetMethod.TargetWeapon(pickableSensor);
            EnnemyController targetEnemy = TargetMethod.TargetEnemy(enemySensor);
        
            if (targetWeapon != null)
            {                
                mover.MoveToward(targetWeapon.transform.position);
                if (targetEnemy != null)
                {
                    handController.SetRotationHand((Vector2)targetEnemy.transform.position);
                    handController.Use();                   
                }
                else
                {
                    handController.SetRotationHand((Vector2)targetWeapon.transform.position);
                }
            }
            else
            {   
                if(targetEnemy != null)
                {
                    mover.Follow(targetEnemy.transform.position, distanceFollow);
                    handController.Use();
                } 
                else
                {
                    TargetMethod.SearchEnemyOrPickable(mover, ref randomSearch);
                    handController.SetRotationHand((Vector2)randomSearch);
                }
            }
        }
    }
}
