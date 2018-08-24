using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Environement;

namespace Playmode.Entity.Senses
{
    public class WallStimilis : MonoBehaviour
    {
        private WallController wallController;

        private void Awake()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            wallController = GetComponentInChildren<WallController>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            other.GetComponent<WallSensor>()?.See(wallController);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            other.GetComponent<WallSensor>()?.LooseSightOf(wallController);
        }


    }

}

