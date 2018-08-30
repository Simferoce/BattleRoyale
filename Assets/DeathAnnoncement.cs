using Playmode.Event;
using Playmode.Util.Values;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeathAnnoncement : MonoBehaviour {

    private EventHandlerEnemyDeath eventHandlerEnemyDeath;
    private Text text;

    private void Awake()
    {
        eventHandlerEnemyDeath = GameObject.FindWithTag(Tags.MainController).GetComponent<EventHandlerEnemyDeath>();

        eventHandlerEnemyDeath.OnEventPublished += EventHandlerEnemyDeath_OnEventPublished;
        text = GetComponent<Text>();
    }

    private void EventHandlerEnemyDeath_OnEventPublished(EnemyDeathData data)
    {
        text.text = data.name + " died.";
    }
}
