using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag.Player
{
    public class PlayerMovement : MonoBehaviour
    {
        public delegate void OnFinishReachHandler();
        public event OnFinishReachHandler onFinishReach;

        private float movementSpeed = 1;

        private bool moveLeft = false;

        private void Update()
        {
            Move();
        }

        private void Move()
        {
            transform.position += movementSpeed * Constants.moveLeftDirection[moveLeft] * Time.deltaTime;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == Constants.FinishTag)
            {
                onFinishReach?.Invoke();
            }
        }

        public void SetMoveSpeed(float speed)
        {
            movementSpeed = speed;
        }

        public void ChangeDirection()
        {
            moveLeft = !moveLeft;
        }
    }
}