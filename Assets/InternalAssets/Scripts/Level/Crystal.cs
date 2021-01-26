using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZigZag;
using ZigZag.Pooling;

namespace ZigZag.Level
{
    public class Crystal : MonoBehaviour, IDisappearable, ICollectable
    {
        [SerializeField]
        private Animator animator;

        public void Collect()
        {
            animator.SetTrigger("Collect");
        }

        public void Disappear()
        {
            animator.SetTrigger("Disappear");
        }
    }
}