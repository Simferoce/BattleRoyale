using Playmode.Entity.Senses;
using Playmode.Util.Values;
using System.Linq;
using Playmode.Pickable;
using UnityEngine;

namespace Playmode.Ennemy.Strategies
{
    public static class TargetMethod
    {
        public static EnnemyController TargetEnemy(EnnemySensor enemySensor)
        {
            if (enemySensor.EnnemiesInSight.Count() > 0)
                return enemySensor.EnnemiesInSight.First();
            else
                return null;
        }
        public static PickableControllerMedKit TargetMedkit(PickableSensor pickableSensor)
        {
            if (pickableSensor.PickablesMedKitInSight.Count() > 0)
                return pickableSensor.PickablesMedKitInSight.First();
            else
                return null;
        }

        public static PickableControllerWeapon TargetWeapon(PickableSensor pickableSensor)
        {
            if (pickableSensor.PickablesWeaponInSight.Count() > 0)
                return pickableSensor.PickablesWeaponInSight.First();
            else
                return null;
        }

        public static Vector2 Search()
        {
            return new Vector2(UnityEngine.Random.Range(-CameraInfo.Width / 2, CameraInfo.Width / 2),
                UnityEngine.Random.Range(-CameraInfo.Height / 2, CameraInfo.Height / 2));
        }
    } 
}
