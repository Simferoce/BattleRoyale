using UnityEngine;

namespace Playmode.Entity.Senses
{
    public delegate void HitSensorEventHandler(int hitPoints, string objectThatHitName);

    public class HitSensor : MonoBehaviour
    {
        public event HitSensorEventHandler OnHit;

        public void Hit(int hitPoints, string objectThatHitName)
        {
            NotifyHit(hitPoints, objectThatHitName);
        }

        private void NotifyHit(int hitPoints, string objectThatHitName)
        {
            OnHit?.Invoke(hitPoints, objectThatHitName);
        }
    }
}