using Playmode.Entity.Senses;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playmode.Pickable
{
    public class PickableSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] pickables;
        private Transform[] spawners;

        private void Awake()
        {
            spawners = new Transform[transform.GetChildCount()];
            for (int i = 0; i < spawners.Length; ++i)
            {
                spawners[i] = transform.GetChild(i);
            }

            SpawnPickable();
        }

        private void SpawnPickable()
        {
            for (int i = 0; i < spawners.Length; ++i)
            {
                int randomPickable = UnityEngine.Random.Range(0, pickables.Length);
                GameObject gameObject = Instantiate(pickables[randomPickable], spawners[i]);
            }
        }
    }

}

