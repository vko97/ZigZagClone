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
        public const int levelCompleteBonus = 500;
        public const int crystalCollectBonus = 10;
        public const int comboBonus = 500;

        public const float basicOpositeDirSpawnChance = 0.3f;
        public const float basicCrystalSpawnChance = 0.1f;
        public const float basicMoveSpeed = 10f;

        public static readonly Vector3 startPos = new Vector3(-17f, .5f, -14f);

        public const string loginScene = "LoginScene";
        public const string mainMenuScene = "MainMenuScene";
        public const string levelScene = "LevelScene";
    }
}