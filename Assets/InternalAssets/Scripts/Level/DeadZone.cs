using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag.Level
{
    public class DeadZone : MonoBehaviour
    {
        public delegate void OnPlayerDeadHandler();
        public event OnPlayerDeadHandler onPlayerDead;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == Constants.PlayerTag)
            {
                onPlayerDead?.Invoke();
            }
        }
    }
}