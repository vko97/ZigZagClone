using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZigZag;
using ZigZag.UI;

namespace ZigZag.Level
{
    public class ScoreController : MonoBehaviour
    {
        public delegate void OnScoreChangeHandler(int score);
        public event OnScoreChangeHandler onScoreChange;

        [SerializeField]
        private ScoreView scoreView;

        private int score = 0;
        private int comboCounter = 0;
        private int levelMultiplier = 1;
        private int level = 1;

        private void Awake()
        {
            onScoreChange += scoreView.SetScore;
        }

        public void OnPlatformPass()
        {
            if (comboCounter < 9)
            {
                comboCounter++;
                return;
            }
            score += Constants.comboBonus * levelMultiplier;
            comboCounter = 0;
            onScoreChange?.Invoke(score);
        }

        public void OnCrystalCollect()
        {
            score += Constants.crystalCollectBonus * levelMultiplier;
            onScoreChange?.Invoke(score);
        }

        public void OnLevelComplete()
        {
            score += Constants.levelCompleteBonus * levelMultiplier * level / 2;
            onScoreChange?.Invoke(score);
        }

        public void SetLevelMultiplier(int mult)
        {
            levelMultiplier = mult;
        }

        public void SetLevel(int level)
        {
            this.level = level;
        }
    }
}