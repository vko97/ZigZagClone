using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZigZag;
using ZigZag.UI;
using TMPro;

namespace ZigZag.Level
{
    public class ScoreController : MonoBehaviour
    {
        public delegate void OnScoreChangeHandler(int score);
        public event OnScoreChangeHandler onScoreChange;

        [SerializeField]
        private ScoreView scoreView;
        [SerializeField]
        private AnimationClip scaleAnimation;
        [SerializeField]
        private TMP_Text newBestText;

        private int score = 0;
        private int basicScore = 0;
        private int crystalScore = 0;
        private int comboCounter = 1;
        private int levelMultiplier = 1;
        private int level = 1;

        private void Awake()
        {
            onScoreChange += scoreView.SetScore;
        }

        private IEnumerator AnimatedCountScore()
        {
            int _score = 0;
            scoreView.SetScore(_score);
            int stepsAmount = 60;
            int step = basicScore / stepsAmount;
            for (int i = 0; i < stepsAmount; i++)
            {
                yield return new WaitForSeconds(scaleAnimation.length / stepsAmount);
                _score += step;
                scoreView.SetScore(_score);
            }
            scoreView.SetScore(basicScore);
            _score = basicScore;
            step = crystalScore / stepsAmount;
            yield return new WaitForSeconds(0.5f);
            for (int i = 0; i < stepsAmount; i++)
            {
                yield return new WaitForSeconds(scaleAnimation.length / stepsAmount);
                _score += step;
                scoreView.SetScore(_score);
            }
            scoreView.SetScore(score);
        }

        private void CalcScore()
        {
            score = basicScore + crystalScore;
        }

        private void CheckAndSetBestScore()
        {
            if (score > PlayerData.Instance().info.bestScore)
            {
                newBestText.gameObject.SetActive(true);
                PlayerData.Instance().info.bestScore = score;
            }
        }

        public void CountScore()
        {
            StartCoroutine(AnimatedCountScore());
            scoreView.PlayScoreCountAnim();
            CheckAndSetBestScore();
        }

        public void OnPlatformPass()
        {
            if (comboCounter < 9)
            {
                comboCounter++;
                return;
            }
            basicScore += Constants.comboBonus * levelMultiplier;
            comboCounter = 0;
            CalcScore();
            onScoreChange?.Invoke(score);
            Debug.Log("10 platform bonus");
        }

        public void OnCrystalCollect()
        {
            crystalScore += Constants.crystalCollectBonus * levelMultiplier;
            CalcScore();
            onScoreChange?.Invoke(score);
        }

        public void OnLevelComplete()
        {

            basicScore += Constants.levelCompleteBonus * levelMultiplier * level;
            CalcScore();
            onScoreChange?.Invoke(score);
            Debug.Log("LEVEL COMPLETE SCORE MANAGER");
        }

        public void Initialize(int level, int levelMultiplier)
        {
            this.level = level;
            this.levelMultiplier = levelMultiplier;
        }

        
    }
}