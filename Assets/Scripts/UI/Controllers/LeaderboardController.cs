using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LeaderboardController : MonoBehaviour
{
    [SerializeField]
    private LeaderView leaderViewPrefab;
    [SerializeField]
    private Transform parent;
    [Range(1,20)]
    [SerializeField]
    private int leadersAmount = 10;
    [SerializeField]
    private PresetsRepository presetsRep;
    [SerializeField]
    private Button backButton;

    private List<LeaderView> leaderViews = new List<LeaderView>();

    private void Awake()
    {
        backButton.onClick.AddListener(() => gameObject.SetActive(false));
    }

    private void OnEnable()
    {
        Initialize();
    }

    private void OnDisable()
    {
        for(int i = 0; i < leaderViews.Count; i++)
        {
            Destroy(leaderViews[i].gameObject);
        }
        leaderViews.Clear();
    }

    private async void Initialize()
    {
        List<PlayerInfo> leaders = await FirebaseController.Instance().LoadLeaders(leadersAmount);
        leadersAmount = Mathf.Min(leadersAmount, leaders.Count);
        for (int i = 0; i < leadersAmount; i++)
        {
            var leader = leaders[i];
            var presetNumber = presetsRep.GetPresetNumber(leader.levelId);
            var level = Utils.CalcLevel(presetNumber, leader.levelMultiplier);
            var leaderView = Instantiate(leaderViewPrefab, parent,false);
          
            
            leaderView.transform.localScale = Vector3.one;
            leaderView.gameObject.SetActive(true);
            var position = leaderView.transform.position;
            position.z = 0;
            leaderView.transform.localPosition = position;

            leaderView.Initialize(i + 1, leader.name, level, leader.bestScore);
            leaderViews.Add(leaderView);
        }
    }

    
}
