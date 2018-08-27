using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Playmode.Entity.Senses
{
    public delegate void PickableSensorEventHandler(Playmode.Pickable.PickableController pickableInfo);

    public class PickableSensor : MonoBehaviour
    {
        private ICollection<Playmode.Pickable.PickableController> pickablesInSight;

        public event PickableSensorEventHandler OnPickableSeen;
        public event PickableSensorEventHandler OnPickableSightLost;

        public IEnumerable<Playmode.Pickable.PickableController> PickablesInSight => pickablesInSight;

        private void Awake()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            pickablesInSight = new HashSet<Playmode.Pickable.PickableController>();
        }

        public void See(Playmode.Pickable.PickableController pickable)
        {
            pickablesInSight.Add(pickable);

            NotifyPickableSeen(pickable);
        }

        public void LooseSightOf(Playmode.Pickable.PickableController pickable)
        {
            pickablesInSight.Remove(pickable);

            NotifyPickableSightLost(pickable);
        }

        private void NotifyPickableSeen(Playmode.Pickable.PickableController pickable)
        {
            OnPickableSeen?.Invoke(pickable);
        }

        private void NotifyPickableSightLost(Playmode.Pickable.PickableController pickable)
        {
            OnPickableSightLost?.Invoke(pickable);
        }
    }
}