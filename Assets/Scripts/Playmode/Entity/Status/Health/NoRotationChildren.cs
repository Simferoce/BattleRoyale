using UnityEngine;

namespace Playmode.Entity.Status.Health
{
    public class NoRotationChildren : MonoBehaviour
    {
        [SerializeField] private float yOffSet = 1.5f;

        void Update()
        {
            this.transform.rotation = Quaternion.identity;
            this.transform.position = this.transform.parent.position + new Vector3(0, yOffSet);
        }
    }
}

