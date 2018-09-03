using UnityEngine;

namespace Playmode.Pickable
{
    public class PickableSpawner : MonoBehaviour
    {
        public GameObject Spawn(GameObject pickable)
        {
            return Instantiate(pickable, this.transform.position, Quaternion.identity);
        }
    }
}