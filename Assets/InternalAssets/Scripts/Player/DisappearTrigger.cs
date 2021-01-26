using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZigZag.Level;

namespace ZigZag.Player
{
    public class DisappearTrigger : MonoBehaviour
    {
        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent<IDisappearable>(out var crystal))
            {
                crystal.Disappear();
            }
        }
    }
}