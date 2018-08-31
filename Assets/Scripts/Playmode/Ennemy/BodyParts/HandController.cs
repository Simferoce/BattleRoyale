using System;
using Playmode.Movement;
using Playmode.Weapon;
using UnityEngine;

namespace Playmode.Ennemy.BodyParts
{
    public class HandController : MonoBehaviour
    {
        private WeaponController weapon;
        private EnnemyController ennemyController;

        private void Awake()
        {
            ennemyController = transform.parent.GetComponentInChildren<EnnemyController>();
        }

        public void Take(GameObject weaponObject)
        {
            if (weaponObject != null)
            {
                WeaponController newWeapon = weaponObject.GetComponentInChildren<WeaponController>();
                if (this.weapon != null && weapon.GetType().Equals(newWeapon.GetType()))
                {
                    this.weapon.UpdatePower();
                    Destroy(weaponObject);
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
                weapon.Shoot(ennemyController);
            }
        }

        public void SetRotationHand(Vector3 position)
        {
            Vector3 dir = position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }
    }
}