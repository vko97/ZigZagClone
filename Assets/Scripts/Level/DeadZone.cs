using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag.Level
{
    public class DeadZone : MonoBehaviour
    {
        public delegate void OnPlayerDeadHandler();
        public event OnPlayerDeadHandler onPlayerDead;

        private Transform target;
        private Vector3 position;

        private void Update()
        {
            FollowTarget();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == Constants.PlayerTag)
            {
                onPlayerDead?.Invoke();
            }
        }

        private void FollowTarget()
        {
            position = target.position;
            position.y = -0.6f;
            transform.position = position;
        }

        public void SetTarget(Transform target)
        {
            this.target = target;
        }
    }
}