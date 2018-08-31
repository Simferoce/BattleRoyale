using System;
using Playmode.Ennemy.Strategies;
using Playmode.Util.Collections;
using UnityEngine;

namespace Playmode.Ennemy
{
    public class EnnemySpawner : MonoBehaviour
    {
        private static readonly Color[] DefaultColors =
        {
            Color.white, Color.black, Color.blue, Color.cyan, Color.green,
            Color.magenta, Color.red, Color.yellow, new Color(255, 125, 0, 255)
        };

        private static readonly string[] DefaultNames =
        {
            "Jonnhy", "Bob", "Leo", "Mike", "Pierre", "Victor", "Vladimir", "Donald", "Yannick"
        };

        private static readonly EnnemyStrategy[] DefaultStrategies =
        {
            EnnemyStrategy.Normal,
            EnnemyStrategy.Careful,
            EnnemyStrategy.Cowboy,
            EnnemyStrategy.Camper
        };

        [SerializeField] private GameObject ennemyPrefab;
        [SerializeField] private Color[] colors = DefaultColors;
        [SerializeField] private string[] names = DefaultNames;

        private void Awake()
        {
            ValidateSerialisedFields();
        }

        private void Start()
        {
            SpawnEnnemies();
        }

        private void ValidateSerialisedFields()
        {
            if (ennemyPrefab == null)
                throw new ArgumentException("Can't spawn null ennemy prefab.");
            if (colors == null || colors.Length == 0)
                throw new ArgumentException("Ennemies needs colors to be spawned.");
            if (transform.childCount <= 0)
                throw new ArgumentException("Can't spawn ennemis whitout spawn points. " +
                                            "Create chilldrens for this GameObject as spawn points.");
        }

        private void SpawnEnnemies()
        {
            var stragegyProvider = new LoopingEnumerator<EnnemyStrategy>(DefaultStrategies);
            var colorProvider = new LoopingEnumerator<Color>(colors);
            var nameProvider = new LoopingEnumerator<string>(names);

            for (var i = 0; i < transform.childCount; i++)
                SpawnEnnemy(
                    transform.GetChild(i).position,
                    stragegyProvider.Next(),
                    colorProvider.Next(),
                    nameProvider.Next()
                );
        }

        private void SpawnEnnemy(Vector3 position, EnnemyStrategy strategy, Color color, string name)
        {
            Instantiate(ennemyPrefab, position, Quaternion.identity)
                .GetComponentInChildren<EnnemyController>()
                .Configure(strategy, color, name);
        }
    }
}