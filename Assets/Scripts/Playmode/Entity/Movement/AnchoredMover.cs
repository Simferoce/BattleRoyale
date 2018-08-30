using UnityEngine;

namespace Playmode.Movement
{
    public class AnchoredMover : Mover
    {
        private Transform rootTransform;

        private new void Awake()
        {
            base.Awake();

            InitializeComponent();
        }

        private void InitializeComponent()
        {
            rootTransform = transform.root;
        }

        public override void Move(Vector3 direction)
        {
            transform.Translate(direction.normalized * speed * Time.deltaTime);
        }

        public override void Rotate(float direction)
        {
            transform.RotateAround(
                rootTransform.position,
                Vector3.forward,
                (direction < 0 ? rotateSpeed : -rotateSpeed) * Time.deltaTime
            );
        }

        public override void MoveToward(Vector3 destination)
        {
            throw new System.NotImplementedException();
        }

        public override void Follow(Vector3 target, float distance)
        {
            throw new System.NotImplementedException();
        }

        public override void SetRotationToLookAt(Vector3 position)
        {
            throw new System.NotImplementedException();
        }

        public override void KeepDistance(Vector3 target, float distance)
        {
            throw new System.NotImplementedException();
        }

        public override void MoveBackward(Vector3 destination)
        {
            throw new System.NotImplementedException();
        }
    }
}