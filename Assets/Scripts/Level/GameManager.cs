using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ZigZag.Player;

namespace ZigZag.Level
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerMovement playerMovement;
        [SerializeField]
        private DeadZone deadZone;
        [SerializeField]
        private TouchInput input;

        private void Awake()
        {
            deadZone.SetTarget(playerMovement.transform);
            deadZone.onPlayerDead += OnPlayerDead;
            input.onPointerDown += OnTappedScreen;
            playerMovement.onFinishReach += OnFinishReach;
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