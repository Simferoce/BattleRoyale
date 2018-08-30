using Playmode.Pickable;
using System.Collections.Generic;
using UnityEngine;

namespace Playmode.Entity.Senses
{
    public delegate void PickableSensorEventHandler(Pickable.PickableController weaponController);

    public class PickableSensor : MonoBehaviour
    {
        private ICollection<Pickable.PickableController> pickablesInSight;

        public event PickableSensorEventHandler OnPickableSeen;
        public event PickableSensorEventHandler OnPickableSightLost;

        public IEnumerable<Pickable.PickableController> PickablesInSight => pickablesInSight;

        private void Awake()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            pickablesInSight = new List<Pickable.PickableController>();
        }

        public void See(Pickable.PickableController pickable)
        {
            pickablesInSight.Add(pickable);
            pickable.OnPickUp += Pickable_OnPickUp;

            NotifyPickableSeen(pickable);
        }

        private void Pickable_OnPickUp(PickableController pickableController)
        {
            pickablesInSight.Remove(pickableController);
        }

        public void LooseSightOf(Pickable.PickableController pickable)
        {      
            pickablesInSight.Remove(pickable);
            pickable.OnPickUp -= Pickable_OnPickUp;
            NotifyPickableSightLost(pickable);
        }

        private void NotifyPickableSeen(Pickable.PickableController pickable)
        {
            OnPickableSeen?.Invoke(pickable);
        }

        private void NotifyPickableSightLost(Pickable.PickableController pickable)
        {
            OnPickableSightLost?.Invoke(pickable);
        }
    }
}