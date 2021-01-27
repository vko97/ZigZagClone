using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace ZigZag.UI
{
    public class LeaderView : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text numberText;
        [SerializeField]
        private TMP_Text nameText;
        [SerializeField]
        private TMP_Text levelText;
        [SerializeField]
        private TMP_Text scoreText;

        public void Initialize(int number, string name, int level, int score)
        {
            numberText.text = number.ToString();
            nameText.text = name;
            levelText.text = level.ToString();
            scoreText.text = score.ToString();
        }
    }
}