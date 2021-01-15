using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZigZag;
using ZigZag.Pooling;

namespace ZigZag.Level
{
    public class Crystal : MonoBehaviour, IDisappearable
    {
        [SerializeField]
        private Animator animator;

        private void OnDisable()
        {
            transform.localScale = Vector3.one;
        }

        public void Collect()
        {
            animator.SetTrigger("Collect");
        }

        void IDisappearable.Disappear()
        {
            animator.SetTrigger("Disappear");
        }
    }
}