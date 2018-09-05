using System;
using UnityEngine;

namespace Playmode.Movement
{
    public class Mover : MonoBehaviour
    {
        private Transform rootTransform;

        public static readonly Vector2 Foward = Vector2.up;
        public const float CLOCKWISE = 1f;

        [SerializeField] protected float speed = 1f;
        public float Speed { get { return speed; } set { speed = value; } }
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

        public void Move(Vector2 direction)
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

        public void MoveToward(Vector2 destination)
        {
            SetRotationToLookAt(destination);

           Move(new Vector2(0, speed * Time.deltaTime));
        }

        public void Follow(Vector2 target, float distance)
        {
            float distanceWithTarget = Vector2.Distance(target, rootTransform.transform.position);
            if(distanceWithTarget >= distance)
            {
                MoveToward(target);
            } else
            {
                SetRotationToLookAt(target);
            }
        }

        public void SetRotationToLookAt(Vector2 position)
        {
            Vector2 dir = position - (Vector2)rootTransform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            rootTransform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
        }

        public void KeepDistance(Vector2 target, float distance)
        {
            float distanceWithTarget = Vector2.Distance(target, rootTransform.transform.position);
            if(distanceWithTarget >= distance)
            {
                MoveToward(target);
            } else
            {
                MoveBackward(target);
            }
        }

        public void MoveBackward(Vector2 destination)
        {
            SetRotationToLookAt(destination);
            Move(new Vector2(0, -speed * Time.deltaTime));
        }
    }
}