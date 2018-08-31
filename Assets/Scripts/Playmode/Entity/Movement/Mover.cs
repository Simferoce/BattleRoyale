using System;
using UnityEngine;

namespace Playmode.Movement
{
    public class Mover : MonoBehaviour
    {
        private Transform rootTransform;

        public static readonly Vector3 Foward = Vector3.up;
        public const float Clockwise = 1f;

        [SerializeField] protected float speed = 1f;
        [SerializeField] protected float rotateSpeed = 90f;

        private void ValidateSerialisedFields()
        {
            if (speed < 0)
                throw new ArgumentException("Speed can't be lower than 0.");
            if (rotateSpeed < 0)
                throw new ArgumentException("RotateSpeed can't be lower than 0.");
        }

        private void Awake()
        {
            ValidateSerialisedFields();
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            rootTransform = transform.root;
        }

        public void Move(Vector3 direction)
        {
            rootTransform.Translate(direction.normalized * speed * Time.deltaTime);
        }

        public void Rotate(float direction)
        {
            rootTransform.Rotate(
                Vector3.forward,
                (direction < 0 ? rotateSpeed : -rotateSpeed) * Time.deltaTime
            );
        }

        public void MoveToward(Vector3 destination)
        {
            SetRotationToLookAt(destination);

           Move(new Vector3(0, speed * Time.deltaTime,0));
        }

        public void Follow(Vector3 target, float distance)
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

        public void SetRotationToLookAt(Vector3 position)
        {
            Vector3 dir = position - rootTransform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            rootTransform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }

        public void KeepDistance(Vector3 target, float distance)
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

        public void MoveBackward(Vector3 destination)
        {
            SetRotationToLookAt(destination);
            Move(new Vector3(0, -speed * Time.deltaTime,0));
        }
    }
}