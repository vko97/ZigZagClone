using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZigZag
{
    public static class Constants
    {
        public static readonly Dictionary<bool, Vector3> moveLeftDirection = new Dictionary<bool, Vector3>
    {
        { true, Vector3.forward},
        { false, Vector3.right}
    };
        public const string DeadZoneTag = "DeadZone";
        public const string PlayerTag = "Player";
        public const string CrystalTag = "Crystal";
        public const string FinishTag = "Finish";
    }
}