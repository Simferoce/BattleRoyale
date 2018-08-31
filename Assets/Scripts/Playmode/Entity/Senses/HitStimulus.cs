using System;
using UnityEngine;

namespace Playmode.Entity.Senses
{
    public class HitStimulus : MonoBehaviour
    {
        [Header("Behaviour")] [SerializeField] private int hitPoints = 10;
        public int HitPoints { get { return hitPoints; } set { hitPoints = value;} }
        public string ShooterName { get; set; }

        private void Awake()
        {
            ValidateSerializeFields();
        }

        private void ValidateSerializeFields()
        {
            if (hitPoints < 0)
                throw new ArgumentException("Hit points can't be less than 0.");
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            HitSensor hitSensor = other.GetComponent<Entity.Senses.HitSensor>();
            if(hitSensor != null)
            {
                hitSensor.Hit(hitPoints, ShooterName);
                Destroy(this.transform.parent.gameObject);
            }


        }
    }
}