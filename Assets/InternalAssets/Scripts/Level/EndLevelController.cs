using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ZigZag;

public class EndLevelController : MonoBehaviour
{
    [SerializeField]
    private Button nextLevelButton;
    [SerializeField]
    private Button mainMenuButton;
    [SerializeField]
    private Button watchAdsButton;
    [SerializeField]
    private TMP_Text nextLevelText;
    [SerializeField]
    private TMP_Text levelResultText;
    [SerializeField]
    private RewardedVideoManager rewardedVideoManager;

    private void Awake()
    {
        nextLevelButton.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
        mainMenuButton.onClick.AddListener(() => SceneManager.LoadScene(Constants.mainMenuScene));
        watchAdsButton.onClick.AddListener(rewardedVideoManager.ShowRewardedVideo);
        rewardedVideoManager.onVideoReward += () => watchAdsButton.gameObject.SetActive(false);
    }

    private void ShowUI()
    {
        nextLevelButton.gameObject.SetActive(true);
        levelResultText.gameObject.SetActive(true);
        mainMenuButton.gameObject.SetActive(true);
        watchAdsButton.gameObject.SetActive(true);
    }

    public void OnLevelComplete()
    {
        ShowUI();
    }

    public void OnLevelFailed()
    {
        nextLevelText.text = "Retry";
        levelResultText.text = "You failed";
        ShowUI();
    }
}
