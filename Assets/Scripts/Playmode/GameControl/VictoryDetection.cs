using Playmode.Ennemy;
using Playmode.Event;
using Playmode.Util.Values;
using UnityEngine;

namespace Playmode.GameControl
{
    public class VictoryDetection : MonoBehaviour
    {

        private EventHandlerEnemyDeath eventHandlerEnemyDeath;
        private EventHandlerVictory eventHandlerVictory;
        private GameObject[] enemyObjects;
        private int numbOfEnemies;

        private void Awake()
        {
            eventHandlerEnemyDeath = GameObject.FindWithTag(Tags.MainController).GetComponent<EventHandlerEnemyDeath>();
            eventHandlerEnemyDeath.OnEventPublished += EventHandlerEnemyDeath_OnEventPublished;
            eventHandlerVictory = GameObject.FindWithTag(Tags.MainController).GetComponent<EventHandlerVictory>();
        }

        private void Start()
        {
            enemyObjects = GameObject.FindGameObjectsWithTag(Tags.Enemy);
            numbOfEnemies = enemyObjects.Length;
            Debug.Log(numbOfEnemies);
        }

        private void EventHandlerEnemyDeath_OnEventPublished(EnemyDeathData data)
        {
            --numbOfEnemies;
            if (numbOfEnemies <= 1)
            {
                string winnerName = "";
                for (int i = 0; i < enemyObjects.Length; ++i)
                {
                    if (enemyObjects[i] != null)
                    {
                        string enemyName = enemyObjects[i].GetComponentInChildren<EnnemyController>().Name;
                        if (enemyName != data.name)
                        {
                            winnerName = enemyName;
                            break;
                        }
                    }
                }
                Debug.Log(enemyObjects);
                eventHandlerVictory.Publish(new VictoryData(winnerName));
            }
        }

        private void OnDisable()
        {
            eventHandlerEnemyDeath.OnEventPublished -= EventHandlerEnemyDeath_OnEventPublished;
        }

    }

}
