using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ZigZag.Player;
using ZigZag.Events;
using System.Linq;

namespace ZigZag.Level
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerMovement playerMovement;
        [SerializeField]
        private PlayerCollect playerCollect;
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

        private void Awake()
        {
            var info = PlayerData.Instance().info;
            var preset = repository.GetPresets().Where(item => item.id == info.levelId).FirstOrDefault();
            platformMaterial.color = preset.platformColor;
            var level = repository.GetPresets().IndexOf(preset) + 1;

            var opositeDirSpawnChance = Utils.OpositeDirectionSpawnChance(info.levelMultiplier);
            var crystalSpawnChance = Utils.CrystalSpawnChance(info.levelMultiplier);
            var platformsNumber = Utils.PlatformNumber(preset.platformNumber, info.levelMultiplier);
            var moveSpeed = Utils.MovementSpeed(info.levelMultiplier);

            scoreController.Initialize(level, info.levelMultiplier);
            if (TryGetComponent<GameEventListener>(out var listener))
            {
                listener.Response.AddListener(scoreController.OnPlatformPass);
            }

            generator.Initialize(platformsNumber, opositeDirSpawnChance, crystalSpawnChance);
            playerMovement.SetMoveSpeed(moveSpeed);
            Debug.Log(moveSpeed);

            deadZone.SetTarget(playerMovement.transform);
            deadZone.onPlayerDead += OnPlayerDead;

            input.onPointerDown += OnTappedScreen;

            playerMovement.onFinishReach += scoreController.OnLevelComplete;
            playerMovement.onFinishReach += OnFinishReach;
            playerCollect.onCrystalCollect += scoreController.OnCrystalCollect;
        }

        private void OnPlayerDead()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //TODO show player dead UI
        }

        private void OnFinishReach()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            //TODO show score UI
        }

        public void OnTappedScreen()
        {
            playerMovement.ChangeDirection();
        }
    }
}