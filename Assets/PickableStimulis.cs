using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Playmode.Entity.Senses
{
    public class PickableStimulis : MonoBehaviour
    {

        private Pickable.PickableInfo pickable;

        private void Awake()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            pickable = transform.root.GetComponentInChildren<Pickable.PickableInfo>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            other.GetComponent<PickableSensor>()?.See(pickable);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            other.GetComponent<PickableSensor>()?.LooseSightOf(pickable);
        }
    }
}
