using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZigZag.Player;

namespace ZigZag.Level
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField]
        private PlayerMovement target;

        private float xDiff;
        private float zDiff;

        private void Start()
        {
            var diff = target.transform.position - transform.position;
            xDiff = diff.x;
            zDiff = diff.z;
        }

        private void Update()
        {
            FollowTarget();
        }

        private void FollowTarget()
        {
            var position = target.transform.position;
            position.x -= xDiff;
            position.y = transform.position.y;
            position.z -= zDiff;
            transform.position = Vector3.Lerp(transform.position, position, .1f);
        }

    }
}