using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MainMenuTopView : MonoBehaviour
{
    [SerializeField]
    private TMP_Text currLevel;
    [SerializeField]
    private TMP_Text bestScore;

    public void Initialize(int level, int score)
    {
        currLevel.text = level.ToString();
        bestScore.text = score.ToString();
    }
}
