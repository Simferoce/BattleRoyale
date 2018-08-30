using Playmode.Event;
using Playmode.Util.Values;
using UnityEngine;
using UnityEngine.UI;

namespace Playmode.UI
{
    public class DeathAnnoncement : MonoBehaviour
    {

        private EventHandlerEnemyDeath eventHandlerEnemyDeath;
        private Text text;

        private void Awake()
        {
            eventHandlerEnemyDeath = GameObject.FindWithTag(Tags.MainController).GetComponent<EventHandlerEnemyDeath>();

            eventHandlerEnemyDeath.OnEventPublished += EventHandlerEnemyDeath_OnEventPublished;
            text = GetComponent<Text>();
        }

        private void OnDisable()
        {
            eventHandlerEnemyDeath.OnEventPublished -= EventHandlerEnemyDeath_OnEventPublished;
        }

        private void EventHandlerEnemyDeath_OnEventPublished(EnemyDeathData data)
        {
            text.text = data.name + " died.";
        }
    }
}

