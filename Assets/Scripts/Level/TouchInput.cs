using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ZigZag.Level
{
    public class TouchInput : MonoBehaviour, IPointerDownHandler
    {
        public delegate void OnPointerDownHandler();
        public event OnPointerDownHandler onPointerDown;

        public void OnPointerDown(PointerEventData eventData)
        {
            onPointerDown?.Invoke();
        }
    }
}