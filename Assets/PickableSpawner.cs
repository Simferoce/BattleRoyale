using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pickable
{



    public class PickableSpawner : MonoBehaviour
    {

        [SerializeField] private GameObject pickablePrefab;
        [Header("Type Images")]
        [SerializeField] private Sprite[] pickables;
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

        private void ValidateSerialisedFields()
        {
            if (pickablePrefab == null)
                throw new ArgumentException("Can't spawn null pickable prefab.");
        }

        private void SpawnPickable()
        {
            for (int i = 0; i < spawners.Length; ++i)
            {
                int randomPickable = UnityEngine.Random.Range(0, pickables.Length);
                GameObject gameObject = Instantiate(pickablePrefab, spawners[i]);

                gameObject.GetComponent<SpriteRenderer>().sprite = pickables[randomPickable];
                gameObject.GetComponent<PickableInfo>().TypePickable = (PickableEnum)randomPickable;
            }
        }
    }

}

