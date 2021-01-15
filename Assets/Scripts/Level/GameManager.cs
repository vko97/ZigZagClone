using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ZigZag.Player;
using ZigZag.Events;

namespace ZigZag.Level
{
    public class GameManager : MonoBehaviour
    {
        [Header("Level set")]
        [SerializeField][Range(1, 10)]
        private int level;
        [SerializeField]
        private int levelMultiplier;
        [Space]
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

        private void Awake()
        {
            deadZone.SetTarget(playerMovement.transform);
            deadZone.onPlayerDead += OnPlayerDead;
            input.onPointerDown += OnTappedScreen;
            playerMovement.onFinishReach += scoreController.OnLevelComplete;
            playerMovement.onFinishReach += OnFinishReach;
            playerCollect.onCrystalCollect += scoreController.OnCrystalCollect;
            if (TryGetComponent<GameEventListener>(out var listener))
            {
                listener.Response.AddListener(scoreController.OnPlatformPass);
            }
            scoreController.SetLevelMultiplier(levelMultiplier);
            scoreController.SetLevel(level);
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