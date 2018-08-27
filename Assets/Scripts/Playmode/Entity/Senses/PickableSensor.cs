using System.Collections;
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
            pickablesInSight = new HashSet<Pickable.PickableController>();
        }

        public void See(Pickable.PickableController pickable)
        {
            pickablesInSight.Add(pickable);

            NotifyPickableSeen(pickable);
        }

        public void LooseSightOf(Pickable.PickableController pickable)
        {
            pickablesInSight.Remove(pickable);

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