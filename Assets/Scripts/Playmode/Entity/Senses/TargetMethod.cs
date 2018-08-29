using Playmode.Entity.Senses;
using Playmode.Util.Values;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Playmode.Pickable;
using UnityEngine;

namespace Assets.Scripts.Playmode.Entity.Senses
{
    public static class TargetMethod
    {
        public static Vector3? TargetEnemy(EnnemySensor enemySensor)
        {
            if (enemySensor.EnnemiesInSight.Count() > 0)
                return enemySensor.EnnemiesInSight.First()?.transform.position;
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

        public static Vector3? TargetWeapon(PickableSensor pickableSensor)
        {
            if (pickableSensor.PickablesWeaponInSight.Count() > 0)
                return pickableSensor.PickablesWeaponInSight.First()?.transform.position;
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
