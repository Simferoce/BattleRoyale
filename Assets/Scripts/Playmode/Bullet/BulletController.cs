using Playmode.Movement;
using UnityEngine;

namespace Playmode.Bullet
{
    public class BulletController : MonoBehaviour
    {
        private Mover mover;

        private void Awake()
        {
            InitialzeComponent();
        }

        private void InitialzeComponent()
        {
            mover = GetComponent<Mover>();
        }

        private void Update()
        {
            Act();
        }


        private void Act()
        {
             mover.Move(Mover.Foward);
        }
    }
}