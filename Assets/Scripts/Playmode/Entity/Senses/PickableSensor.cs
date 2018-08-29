using Playmode.Pickable;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Playmode.Entity.Senses
{
    public delegate void PickableSensorEventHandler(Pickable.PickableController weaponController);

    public class PickableSensor : MonoBehaviour
    {
        private ICollection<Pickable.PickableControllerWeapon> pickablesWeaponInSight;
        private ICollection<Pickable.PickableControllerMedKit> pickablesMedKitInSight;

        public event PickableSensorEventHandler OnPickableSeen;
        public event PickableSensorEventHandler OnPickableSightLost;

        public IEnumerable<Pickable.PickableControllerWeapon> PickablesWeaponInSight => pickablesWeaponInSight;
        public IEnumerable<Pickable.PickableControllerMedKit> PickablesMedKitInSight => pickablesMedKitInSight;

        private void Awake()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            pickablesWeaponInSight = new HashSet<Pickable.PickableControllerWeapon>();
            pickablesMedKitInSight = new HashSet<Pickable.PickableControllerMedKit>();
        }

        public void See(Pickable.PickableController pickable)
        {
            if (pickable is PickableControllerWeapon)
                pickablesWeaponInSight.Add(pickable as PickableControllerWeapon);
            else if (pickable is PickableControllerMedKit)
                pickablesMedKitInSight.Add(pickable as PickableControllerMedKit);

            pickable.OnPickUp += Pickable_OnPickUp;

            NotifyPickableSeen(pickable);
        }

        private void Pickable_OnPickUp(PickableController pickableController)
        {
            if (pickableController is PickableControllerWeapon)
                pickablesWeaponInSight.Remove(pickableController as PickableControllerWeapon);
            else if (pickableController is PickableControllerMedKit)
                pickablesMedKitInSight.Remove(pickableController as PickableControllerMedKit);
        }

        public void LooseSightOf(Pickable.PickableController pickable)
        {
            if (pickable is PickableControllerWeapon)
                pickablesWeaponInSight.Remove(pickable as PickableControllerWeapon);
            else if (pickable is PickableControllerMedKit)
                pickablesMedKitInSight.Remove(pickable as PickableControllerMedKit);

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