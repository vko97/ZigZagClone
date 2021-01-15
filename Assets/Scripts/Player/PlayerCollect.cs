using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZigZag.Level;

namespace ZigZag.Player
{
    public class PlayerCollect : MonoBehaviour
    {
        public delegate void OnCrystalCollectHandler();
        public event OnCrystalCollectHandler onCrystalCollect;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent<Crystal>(out var crystal))
            {
                onCrystalCollect?.Invoke();
                crystal.Collect();
            }
        }
    }
}