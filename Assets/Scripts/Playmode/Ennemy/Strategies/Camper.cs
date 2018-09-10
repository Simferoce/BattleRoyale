using Playmode.Ennemy.BodyParts;
using Playmode.Entity.Senses;
using Playmode.Entity.Status.Health;
using Playmode.Movement;
using Playmode.Pickable;
using UnityEngine;

namespace Playmode.Ennemy.Strategies
{
    public class Camper : IEnnemyStrategy
    {
        //BEN_REVIEW : Ces constantes auraient pu être envoyés à la construction à la place. Cela vous aurait permis de
        //             le configurer plus facilement.
        private const int SECONDS_TO_PICK_MEDKIT = 10;
        private const float MEDKIT_DISTANCE = 1.0f;
        
        private readonly Mover mover;
        private readonly HandController handController;
        private readonly EnnemySensor enemySensor;
        private readonly PickableSensor pickableSensor;
        private readonly Health health;
        private PickableControllerMedKit pickableMedkit;
        private Vector2? randomSearch = null;      
        private static float timeAtBeginning;        

        public Camper(Mover mover, HandController handController, EnnemySensor enemySensor, PickableSensor pickableSensor, Health health)
        {
            this.mover = mover;
            this.handController = handController;
            this.health = health;
            timeAtBeginning = Time.time;

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

            //BEN_INFO : Comportement bonus.
            if (Time.time - timeAtBeginning > SECONDS_TO_PICK_MEDKIT)
            {
                //BEN_CORRECTION : La méthode Act est beaucoup trop complexe. Vous auriez pu la diviser en sous méthodes assez
                //                 facilement. Me voir si vous désirez savoir comment.
                if (pickableMedkit != null)
                {               
                    if (Vector2.Distance(pickableMedkit.transform.position,mover.transform.position) < MEDKIT_DISTANCE)
                    {
                        if (health.HealthPoints > 30) //BEN_CORRECTION : Chiffre magique.
                        {
                            EnnemyController targetEnemy = TargetMethod.TargetEnemy(enemySensor);
                            if (targetEnemy == null)
                            {                           
                                mover.Rotate(-1);
                            }
                            else
                            {
                                mover.SetRotationToLookAt(targetEnemy.transform.position);
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
                    PickableControllerWeapon pickableWeapon = TargetMethod.TargetWeapon(pickableSensor);
                    if (pickableWeapon != null)
                    {
                        mover.MoveToward(pickableWeapon.transform.position);
                    }
                    else
                    {
                        //BEN_CORRECTION : Je comprends pas vraiment pouquoi vous avez mis ce bout de code dans une classe statique
                        //                 et non pas dans une classe de base, auquel toutes les autres stratégies auraient hérité.
                        TargetMethod.SearchEnemyOrPickable(mover, ref randomSearch);
                    }
                } 
            }
            else
            {
                if (pickableMedkit != null)
                {
                    mover.MoveToward(pickableMedkit.transform.position);
                }
                else
                {
                    TargetMethod.SearchEnemyOrPickable(mover, ref randomSearch);
                }
            }
        }

        private void OnPickUp(PickableController controller)
        {
            pickableMedkit.OnPickUp -= OnPickUp;
            pickableMedkit = null;
        }
    }
}