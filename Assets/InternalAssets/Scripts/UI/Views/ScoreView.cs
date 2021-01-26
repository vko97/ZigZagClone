using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ZigZag.UI
{
    public class ScoreView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text scoreText;
        [SerializeField]
        public Animator animator;

        public void SetScore(int score)
        {
            scoreText.text = score.ToString();
        }

        public void PlayScoreCountAnim()
        {
            animator.SetTrigger("Count");
        }
    }
}