using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollower : MonoBehaviour
{
    private Transform target;
    private Vector3 position;

    private void Update()
    {
        FollowTarget();
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
