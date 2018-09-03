using Playmode.Entity.Senses;
using Playmode.Util.Values;
using System.Linq;
using Playmode.Pickable;
using UnityEngine;
using Playmode.Movement;
using Playmode.Util.Collections;

namespace Playmode.Ennemy.Strategies
{
    public static class TargetMethod
    {
        private static float proximitySensibility = 0.5f;

        public static EnnemyController TargetEnemy(EnnemySensor enemySensor)
        {
            if (enemySensor.EnnemiesInSight.Count() > 0)
                return enemySensor.EnnemiesInSight.First();
            else
                return null;
        }

        public static EnnemyController TargetEnemyNotZombie(EnnemySensor enemySensor)
        {
            if (enemySensor.EnnemiesInSight.Count() > 0)
            {
                var enemyProvider = new LoopingEnumerator<EnnemyController>(enemySensor.EnnemiesInSight);
                int test = enemySensor.EnnemiesInSight.Count();
                for (var i = 0; i < test; i++)
                {
                    EnnemyController controller = enemyProvider.Next();
                    if (!(controller.GetStrategyType() is ZombieStrategy))
                    {
                        return controller;
                    }
                }
            }
            return null;
        }

        public static PickableControllerMedKit TargetMedkit(PickableSensor pickableSensor)
        {
            if (pickableSensor.PickablesInSight.Count() > 0)
            {
                var pickableProvider = new LoopingEnumerator<PickableController>(pickableSensor.PickablesInSight);
                int test = pickableSensor.PickablesInSight.Count();
                for (var i = 0; i < test; i++)
                {
                    PickableController controller = pickableProvider.Next();
                    if (controller is PickableControllerMedKit)
                    {
                        return controller as PickableControllerMedKit;
                    }
                }
            }

            return null;
        }

        public static PickableControllerWeapon TargetWeapon(PickableSensor pickableSensor)
        {
            if (pickableSensor.PickablesInSight.Count() > 0)
            {
                var pickableProvider = new LoopingEnumerator<PickableController>(pickableSensor.PickablesInSight);
                int test = pickableSensor.PickablesInSight.Count();
                for (var i = 0; i < test; i++)
                {
                    PickableController controller = pickableProvider.Next();
                    if (controller is PickableControllerWeapon)
                    {
                        return controller as PickableControllerWeapon;
                    }
                }
            }            
            return null;
        }

        public static Vector2 Search()
        {
            return new Vector2(UnityEngine.Random.Range(-CameraInfo.Width / 2, CameraInfo.Width / 2),
                UnityEngine.Random.Range(-CameraInfo.Height / 2, CameraInfo.Height / 2));
        }
        public static void SearchEnemyOrPickable(Mover mover, ref Vector2? randomSearch)
        {
            if (randomSearch == null)
                randomSearch = Search();
            else if ((randomSearch - mover.transform.position).Value.magnitude < proximitySensibility)
                randomSearch = Search();
            mover.MoveToward((Vector2) randomSearch);
        }
    } 
}
