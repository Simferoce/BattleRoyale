using Playmode.Event;
using Playmode.Util.Values;
using UnityEngine;
using UnityEngine.UI;

public class VictoryAnnoncement : MonoBehaviour {
    private EventHandlerVictory eventHandlerVictory;
    private Text text;

    private void Awake()
    {
        eventHandlerVictory = GameObject.FindWithTag(Tags.MainController).GetComponent<EventHandlerVictory>();
        text = GetComponent<Text>();

        eventHandlerVictory.OnEventPublished += EventHandlerVictory_OnEventPublished;
    }

    private void EventHandlerVictory_OnEventPublished(VictoryData data)
    {
        text.text = data.WinnerName + " won.";
    }
}
