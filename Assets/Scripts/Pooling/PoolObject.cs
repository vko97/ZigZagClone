using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag.Pooling
{
    public class PoolObject : MonoBehaviour
    {
        public void ReturnToPool()
        {
            gameObject.SetActive(false);
        }
    }
}