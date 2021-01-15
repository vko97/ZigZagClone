using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZigZag.Events;
using ZigZag.Player;

namespace ZigZag.Level
{
    public class Platform : MonoBehaviour
    {
        [SerializeField]
        private GameEvent onPlatformFall;

        private Animator animator;

        private void Awake()
        {
            animator = gameObject.GetComponent<Animator>();
        }

        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.tag == Constants.PlayerTag)
            {
                onPlatformFall.Raise();
                animator.SetTrigger("Fall");
            }
        }
    }
}