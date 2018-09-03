using Playmode.Event;
using Playmode.Util.Values;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Playmode.UI
{
    public class DeathAnnoncement : MonoBehaviour
    {

        private EventHandlerEnemyDeath eventHandlerEnemyDeath;
        private Text text;
        private List<EnemyDeathData> textBuffer;

        private void Awake()
        {
            eventHandlerEnemyDeath = GameObject.FindWithTag(Tags.GameController).GetComponent<EventHandlerEnemyDeath>();

            eventHandlerEnemyDeath.OnEventPublished += EventHandlerEnemyDeath_OnEventPublished;
            text = GetComponent<Text>();
            textBuffer = new List<EnemyDeathData>();
        }

        private void OnDisable()
        {
            eventHandlerEnemyDeath.OnEventPublished -= EventHandlerEnemyDeath_OnEventPublished;
        }

        private void EventHandlerEnemyDeath_OnEventPublished(EnemyDeathData data)
        {
            if(text.text == "")
                ShowText(data);
            else
                textBuffer.Add(data);
        }

        private IEnumerator ClearText()
        {
            yield return new WaitForSeconds(2);
            text.text = "";
            if(textBuffer.Count > 0)
            {
                ShowText(textBuffer[0]);
                textBuffer.Remove(textBuffer[0]);
            }
        }

        private void ShowText(EnemyDeathData data)
        {
            text.text = data.killerName + " killed " + data.name + ".";
            StartCoroutine("ClearText");
        }
    }
}

