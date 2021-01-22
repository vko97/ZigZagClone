using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using ZigZag;
using System.Linq;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private MainMenuTopView topView;
    [SerializeField]
    private PresetsRepository presetsRep;
    [Header("Buttons")]
    [SerializeField]
    private Button playButton;
    [SerializeField]
    private Button removeAdsButton;
    [SerializeField]
    private Button leadesButton;
    [SerializeField]
    private Button optionsButton;
    [SerializeField]
    private Button exitButton;
    [Space]
    [SerializeField]
    private OptionsController options;
    [SerializeField]
    private LeaderboardController leaderboard;
    [SerializeField]
    private IAPManager purchaser;



    private void Awake()
    {
        var info = PlayerData.Instance().info;
        var preset = presetsRep.GetPresets().Where(item => item.id == info.levelId).FirstOrDefault();
        int presetNumber = presetsRep.GetPresetNumber(info.levelId);
        int level = Utils.CalcLevel(presetNumber, info.levelMultiplier);
        topView.Initialize(level, info.bestScore);
        Initialize();

        playButton.onClick.AddListener(() => SceneManager.LoadScene(Constants.levelScene));
        removeAdsButton.onClick.AddListener(() => purchaser.BuyProductById(IAPManager.removeAds));
        leadesButton.onClick.AddListener(() => leaderboard.gameObject.SetActive(true));
        optionsButton.onClick.AddListener(() => options.gameObject.SetActive(true));
        exitButton.onClick.AddListener(() => Application.Quit());

        PlayerData.Instance().onAdsRemove += Initialize;
    }

    private void Initialize()
    {
        if (PlayerData.Instance().info.adsRemoved)
        {
            removeAdsButton.gameObject.SetActive(false);
        }
    }
}
