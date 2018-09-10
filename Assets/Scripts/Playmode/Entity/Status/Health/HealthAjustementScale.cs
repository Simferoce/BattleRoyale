using UnityEngine;

namespace Playmode.Entity.Status.Health
{
    //BEN_CORRECTION : Ça ma pris du temps avant de comprendre que c'était pour votre HealthBar.
    //
    //                 Alors ma question est : pourquoi pas juste "HealthBar" ? Nommage.
    public class HealthAjustementScale : MonoBehaviour
    {

        private Health health;
        private float baseScale;

        private void Awake()
        {
            health = this.transform.parent.GetComponentInChildren<Health>();
            health.OnHealthChange += Health_OnHealthChange; //BEN_CORRECTION : Abonnement à l'événement devrait se faire dans "OnEnable".
            baseScale = this.transform.localScale.x;
        }

        private void OnDisable()
        {
            health.OnHealthChange -= Health_OnHealthChange;
        }

        //BEN_CORRECTION : ScaleModifier n'indique en rien en quoi consiste la valeur. C'est un pourcentage ?
        //                 Un multiplicateur ? Soyez plus précis SVP.
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

