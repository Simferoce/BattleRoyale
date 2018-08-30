using System.Collections;
using System.Collections.Generic;
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