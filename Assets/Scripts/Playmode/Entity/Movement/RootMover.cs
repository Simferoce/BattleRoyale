using UnityEngine;

namespace Playmode.Movement
{
    public class RootMover : Mover
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
            rootTransform.Translate(direction.normalized * speed * Time.deltaTime);
        }

        public override void Rotate(float direction)
        {
            rootTransform.Rotate(
                Vector3.forward,
                (direction < 0 ? rotateSpeed : -rotateSpeed) * Time.deltaTime
            );
        }

        public override void MoveToward(Vector3 destination)
        {
            SetRotationToLookAt(destination);

           Move(new Vector3(0, speed * Time.deltaTime,0));
        }

        public override void Follow(Vector3 target, float distance)
        {
            float distanceWithTarget = Vector2.Distance((Vector2)target, (Vector2)rootTransform.transform.position);
            if(distanceWithTarget >= distance)
            {
                MoveToward(target);
            } else
            {
                SetRotationToLookAt(target);
            }
        }

        public override void SetRotationToLookAt(Vector3 position)
        {
            Vector3 dir = position - rootTransform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            rootTransform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }

        public override void KeepDistance(Vector3 target, float distance)
        {
            float distanceWithTarget = Vector2.Distance((Vector2)target, (Vector2)rootTransform.transform.position);
            if(distanceWithTarget >= distance)
            {
                MoveToward(target);
            } else
            {
                MoveBackward(target);
            }
        }

        public override void MoveBackward(Vector3 destination)
        {
            SetRotationToLookAt(destination);
            Move(new Vector3(0, -speed * Time.deltaTime,0));
        }
    }
}