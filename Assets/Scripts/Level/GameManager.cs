using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ZigZag.Player;
using ZigZag.Events;
using System.Linq;
using TMPro;

namespace ZigZag.Level
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerMovement playerMovement;
        [SerializeField]
        private PlayerCollect playerCollect;
        [SerializeField]
        private List<TargetFollower> playerFollowers;
        [SerializeField]
        private DeadZone deadZone;
        [SerializeField]
        private TouchInput input;
        [SerializeField]
        private ScoreController scoreController;
        [SerializeField]
        private Material platformMaterial;
        [SerializeField]
        private PresetsRepository repository;
        [SerializeField]
        private LevelGenerator generator;
        [SerializeField]
        private DisappearTrigger disappearer;
        [SerializeField]
        private EndLevelController endLevelController;

        private PlayerInfo info;
        private Preset preset;
        private int level;
        

        private void Awake()
        {
            info = PlayerData.Instance().info;
            preset = repository.GetPresets().Where(item => item.id == info.levelId).FirstOrDefault();
            platformMaterial.color = preset.platformColor;
            level = repository.GetPresets().IndexOf(preset) + 1;

            var opositeDirSpawnChance = Utils.OpositeDirectionSpawnChance(info.levelMultiplier);
            var crystalSpawnChance = Utils.CrystalSpawnChance(info.levelMultiplier);
            var platformsNumber = Utils.PlatformNumber(preset.platformNumber, info.levelMultiplier);
            var moveSpeed = Utils.MovementSpeed(info.levelMultiplier);

            scoreController.Initialize(level, info.levelMultiplier);
            if (TryGetComponent<GameEventListener>(out var listener))
            {
                listener.Response.AddListener(scoreController.OnPlatformPass);
                listener.Response.AddListener(generator.AddPlatform);
            }

            generator.Initialize(platformsNumber, opositeDirSpawnChance, crystalSpawnChance);
            playerMovement.SetMoveSpeed(moveSpeed);

            for (int i = 0; i < playerFollowers.Count; i++)
            {
                playerFollowers[i].SetTarget(playerMovement.transform);
            }

            deadZone.onPlayerDead += endLevelController.OnLevelFailed;
            deadZone.onPlayerDead += scoreController.CountScore;
            deadZone.onPlayerDead += OnPlayerDead;

            input.onPointerDown += OnTappedScreen;

            playerMovement.onFinishReach += endLevelController.OnLevelComplete;
            playerMovement.onFinishReach += scoreController.OnLevelComplete;
            playerMovement.onFinishReach += scoreController.CountScore;
            playerMovement.onFinishReach += OnFinishReach;
            playerCollect.onCrystalCollect += scoreController.OnCrystalCollect;

            
        }

        private void SetNextLevel()
        {
            var presets = repository.GetPresets();
            if (level == presets.Count)
            {
                PlayerData.Instance().info.levelMultiplier += 1;
                PlayerData.Instance().info.levelId = presets[0].id;
            }
            else
            {
                PlayerData.Instance().info.levelId = presets[level].id;
            }
        }

        private void OnPlayerDead()
        {
            playerMovement.SetMoveSpeed(0);
            playerMovement.transform.position = Constants.startPos;
            PlayerData.Instance().SaveData();
        }

        private void OnFinishReach()
        {
            playerMovement.SetMoveSpeed(0);
            disappearer.transform.localScale = new Vector3(.3f, 1f, .3f);
            SetNextLevel();
            PlayerData.Instance().SaveData();
        }

        public void OnTappedScreen()
        {
            playerMovement.ChangeDirection();
        }
    }
}