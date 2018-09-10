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
        private bool gameIsFinish = false;

        private void Awake()
        {
            eventHandlerEnemyDeath = GameObject.FindWithTag(Tags.GameController).GetComponent<EventHandlerEnemyDeath>();
            eventHandlerEnemyDeath.OnEventPublished += EventHandlerEnemyDeath_OnEventPublished; //BEN_CORRECTION : Devrait se faire dans "OnEnable". Voir votre méthode "OnDisable".
            eventHandlerVictory = GameObject.FindWithTag(Tags.GameController).GetComponent<EventHandlerVictory>();
        }

        private void Start()
        {
            enemyObjects = GameObject.FindGameObjectsWithTag(Tags.Enemy);
            numbOfEnemies = enemyObjects.Length;
        }

        private void EventHandlerEnemyDeath_OnEventPublished(EnemyDeathData data)
        {
            --numbOfEnemies;
            if (numbOfEnemies <= 1 && gameIsFinish == false)
            {
                gameIsFinish = true;
                //BEN_CORRECTION : Cette boucle est complètement inutile. Lisez bien comme il faut.
                string winnerName = null;
                for (int i = 0; i < enemyObjects.Length; ++i)
                {
                    if (enemyObjects[i] != null)
                    {
                        string enemyName = enemyObjects[i].name;
                        if (enemyName != data.name)
                        {
                            winnerName = enemyName;
                            break;
                        }
                    }
                }
                eventHandlerVictory.Publish(new VictoryData(winnerName));
            }
        }

        private void OnDisable()
        {
            eventHandlerEnemyDeath.OnEventPublished -= EventHandlerEnemyDeath_OnEventPublished;
        }

    }

}
