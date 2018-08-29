using System;
using Playmode.Movement;
using Playmode.Weapon;
using UnityEngine;

namespace Playmode.Ennemy.BodyParts
{
    public class HandController : MonoBehaviour
    {
        private Mover mover;
        private WeaponController weapon;

        private void Awake()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            mover = GetComponent<AnchoredMover>();
        }
        
        public void Hold(GameObject weaponObject)
        {
            if (weaponObject != null)
            {

                WeaponController newWeapon = weaponObject.GetComponentInChildren<WeaponController>();
                if (this.weapon != null && weapon.GetType().Equals(newWeapon.GetType()))
                {
                    this.weapon.UpdatePower();
                }
                else if(this.weapon != null)
                {
                    weaponObject.transform.parent = transform;
                    weaponObject.transform.localPosition = Vector3.zero;
                    weaponObject.transform.localRotation = Quaternion.identity;

                    Destroy(weapon.transform.parent.gameObject);

                    this.weapon = newWeapon;
                }
                else
                {
                    weaponObject.transform.parent = transform;
                    weaponObject.transform.localPosition = Vector3.zero;
                    weaponObject.transform.localRotation = Quaternion.identity;
                    this.weapon = newWeapon;
                }
            }
        }

        public void Use()
        {
            if (weapon != null)
            {               
                weapon.Shoot();                  
            }
        }
    }
}