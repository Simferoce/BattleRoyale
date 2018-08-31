using UnityEngine;

namespace Playmode.Entity.Status.Health
{
    public class HealthAjustementScale : MonoBehaviour
    {

        private Health health;
        private float baseScale;

        private void Awake()
        {
            health = this.transform.parent.GetComponentInChildren<Health>();
            health.OnHealthChange += Health_OnHealthChange;
            baseScale = this.transform.localScale.x;
        }

        private void OnDisable()
        {
            health.OnHealthChange -= Health_OnHealthChange;
        }

        private void Rescale(float xScaleModifier)
        {
            this.transform.localScale = new Vector3(baseScale * xScaleModifier, this.transform.localScale.y);
        }

        private void Health_OnHealthChange(int newHealth)
        {
            Rescale(newHealth / 100f);
        }
    }
}

