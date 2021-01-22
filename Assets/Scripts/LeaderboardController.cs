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
        var leaders = parent.GetComponentsInChildren<LeaderView>();
        foreach(var leader in leaders)
        {
            Destroy(leader);
        }
    }

    private async void Initialize()
    {
        List<PlayerInfo> leaders = await FirebaseController.Instance().LoadLeaders(leadersAmount);
        leadersAmount = Mathf.Min(leadersAmount, leaders.Count);
        for (int i = 0; i < leadersAmount; i++)
        {
            var leader = leaders[i];
            var level = (presetsRep.GetPresetNumber(leader.levelId) + 1) * leader.levelMultiplier;
            var leaderView = Instantiate(leaderViewPrefab, parent,false);
          
            
            leaderView.transform.localScale = Vector3.one;
            leaderView.gameObject.SetActive(true);
            var position = leaderView.transform.position;
            position.z = 0;
            leaderView.transform.localPosition = position;

            leaderView.Initialize(i + 1, leader.name, level, leader.bestScore);
        }
    }

    
}
