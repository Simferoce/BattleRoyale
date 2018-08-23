using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Playmode.Entity.Senses
{
    public delegate void PickableSensorEventHandler(Pickable.PickableInfo pickableInfo);

    public class PickableSensor : MonoBehaviour
    {
        private ICollection<Pickable.PickableInfo> pickablesInSight;

        public event PickableSensorEventHandler OnPickableSeen;
        public event PickableSensorEventHandler OnPickableSightLost;

        public IEnumerable<Pickable.PickableInfo> PickablesInSight => pickablesInSight;

        private void Awake()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            pickablesInSight = new HashSet<Pickable.PickableInfo>();
        }

        public void See(Pickable.PickableInfo pickable)
        {
            pickablesInSight.Add(pickable);

            NotifyPickableSeen(pickable);
        }

        public void LooseSightOf(Pickable.PickableInfo pickable)
        {
            pickablesInSight.Remove(pickable);

            NotifyPickableSightLost(pickable);
        }

        private void NotifyPickableSeen(Pickable.PickableInfo pickable)
        {
            OnPickableSeen?.Invoke(pickable);
        }

        private void NotifyPickableSightLost(Pickable.PickableInfo pickable)
        {
            OnPickableSightLost?.Invoke(pickable);
        }
    }
}