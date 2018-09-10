using UnityEngine;

namespace Playmode.Entity.Status.Health
{
    public class NoRotationChildren : MonoBehaviour
    {
        //BEN_REVIEW : Vous auriez aussi pu prendre carrément un "Vector3".
        [SerializeField] private float yOffSet = 1.5f;

        //BEN_CORRECTION : Mot cké "private" manquant.
        void Update()
        {
            this.transform.rotation = Quaternion.identity;
            //BEN_REVIEW : La position aurait pu être assignée qu'une seule fois au Awake. Voir "localPosition".
            this.transform.position = this.transform.parent.position + new Vector3(0, yOffSet);
        }
    }
}

