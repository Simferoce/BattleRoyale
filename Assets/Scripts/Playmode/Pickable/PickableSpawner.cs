using UnityEngine;

namespace Playmode.Pickable
{
    public class PickableSpawner : MonoBehaviour
    {
        public void Spawn(GameObject pickable)
        {
            Instantiate(pickable, this.transform.position, Quaternion.identity);
        }
    }
}