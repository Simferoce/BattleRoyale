using UnityEngine;

namespace Playmode.Event
{
    public delegate void EventChannelEventHandler<T>(T data);

    public abstract class EventChannel<T> : MonoBehaviour
    {
        public event EventChannelEventHandler<T> OnEventPublished;

        public void Publish(T data)
        {
            OnEventPublished?.Invoke(data);
        }
    }
}


