using Playmode.Event;
using Playmode.Util.Values;
using UnityEngine;
using UnityEngine.UI;

namespace Playmode.UI
{
    public class VictoryAnnoncement : MonoBehaviour
    {
        private EventHandlerVictory eventHandlerVictory;
        private Text text;

        private void Awake()
        {
            eventHandlerVictory = GameObject.FindWithTag(Tags.GameController).GetComponent<EventHandlerVictory>();
            text = GetComponent<Text>();

            eventHandlerVictory.OnEventPublished += EventHandlerVictory_OnEventPublished;
        }

        private void OnDisable()
        {
            eventHandlerVictory.OnEventPublished -= EventHandlerVictory_OnEventPublished;
        }

        //BEN_REVIEW : Je déteste ces noms d'événements à la "Visual Studio". C'est long pour rien....
        private void EventHandlerVictory_OnEventPublished(VictoryData data)
        {
            if (data.WinnerName != null)
                text.text = data.WinnerName + " won !";
            else
                text.text = "Draw.";
        }
    }
}

