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



    private void Awake()
    {
        var info = PlayerData.Instance().info;
        var preset = presetsRep.GetPresets().Where(item => item.id == info.levelId).FirstOrDefault();
        int currLevel = presetsRep.GetPresets().IndexOf(preset) + 1;
        topView.Initialize(currLevel * info.levelMultiplier, info.bestScore);

        playButton.onClick.AddListener(() => SceneManager.LoadScene(Constants.levelScene));

        optionsButton.onClick.AddListener(() => options.gameObject.SetActive(true));
        exitButton.onClick.AddListener(() => Application.Quit());

        Leaders();
    }

    private async void Leaders()
    {
        var leaders =  await FirebaseController.Instance().LoadLeaders();
    }
}
