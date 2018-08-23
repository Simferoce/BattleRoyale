using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Pickable
{
    public enum PickableEnum
    {
        MedicalKit,
        UZI,
        Shotgun,
        Length
    }

    public class PickableInfo : MonoBehaviour {

        public PickableEnum TypePickable { set; get; }

    }
}