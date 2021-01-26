using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZigZag.Events;
using ZigZag.Player;

namespace ZigZag.Level
{
    public class Platform : MonoBehaviour, IDisappearable
    {
        [SerializeField]
        private GameEvent onPlatformFall;

        private Animator animator;

        public void Disappear()
        {
            onPlatformFall.Raise();
            animator.SetTrigger("Fall");
        }

        private void Awake()
        {
            animator = gameObject.GetComponent<Animator>();
        }
    }
}