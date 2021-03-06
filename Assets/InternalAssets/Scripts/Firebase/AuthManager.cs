﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ZigZag.Firebase
{
    public class AuthManager : MonoBehaviour
    {
        private void Awake()
        {
            //Application.targetFrameRate = 60;
            FirebaseController.Instance().onLogin += OnLogin;
            PlayerData.Instance().onLoadedData += LoadMainMenu;
        }

        private void OnLogin()
        {
            PlayerData.Instance().LoadData();
        }

        private void LoadMainMenu()
        {
            SceneManager.LoadScene("MainMenuScene");
        }


    }
}