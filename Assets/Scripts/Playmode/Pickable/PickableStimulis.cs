using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Playmode.Pickable;

namespace Playmode.Entity.Senses
{

    public class PickableStimulis : MonoBehaviour
    {
        private PickableController controller;

        private void Awake()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            controller = GetComponentInChildren<PickableController>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            other.GetComponent<PickableSensor>()?.See(controller);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            other.GetComponent<PickableSensor>()?.LooseSightOf(controller);
        }
    }
}
