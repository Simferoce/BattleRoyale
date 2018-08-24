using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playmode.Pickable
{
    public enum PickableEnum
    {
        MedicalKit,
        UZI,
        Shotgun,
        Length
    }
    public class PickableController : MonoBehaviour
    {
        [SerializeField] private PickableEnum pickable;
        public PickableEnum TypePickable { set; get; }
    }
}

