using UnityEngine;

namespace Playmode.Entity.Senses
{

    public class PickableStimulis : MonoBehaviour
    {
        private Pickable.PickableController controller;

        private void Awake()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            controller = GetComponentInChildren<Pickable.PickableController>();
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
