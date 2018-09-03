using Playmode.Util.Values;
using UnityEngine;

namespace Playmode.Pickable
{
    public class PickableFactory : MonoBehaviour
    {
        [SerializeField] private GameObject[] pickables;
        [SerializeField] private GameObject spawner;
        [SerializeField] private int numbOfSpawners = 0;
        private GameObject[] spawners;

        private void Awake()
        {
            if(numbOfSpawners > 0)
            {
                spawners = new GameObject[numbOfSpawners];
                for(int i = 0; i < spawners.Length; ++i)
                {
                    spawners[i] = Instantiate(spawner,
                        new Vector3(
                            Random.Range(-CameraInfo.Width/2, CameraInfo.Width/2), 
                            Random.Range(-CameraInfo.Height/2, CameraInfo.Height/2)),
                        Quaternion.identity);

                    SpawnPickable(spawners[i].GetComponent<PickableSpawner>());
                }
            }
        }

        private void SpawnPickable(PickableSpawner pickableSpawner)
        {
            GameObject pickable = pickableSpawner.Spawn(pickables[Random.Range(0, pickables.Length)]);
            pickable.transform.parent = pickableSpawner.transform;
        }
    }
}

