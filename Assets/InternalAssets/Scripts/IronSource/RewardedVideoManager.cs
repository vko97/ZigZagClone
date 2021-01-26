using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RewardedVideoManager : MonoBehaviour
{
    public delegate void OnVideoRewardHandler();
    public event OnVideoRewardHandler onVideoReward;

    private void Start()
    {
        IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent;
    }

    private void OnDestroy()
    {
        onVideoReward = null;
        IronSourceEvents.onRewardedVideoAdRewardedEvent -= RewardedVideoAdRewardedEvent;
    }

    private void RewardedVideoAdRewardedEvent(IronSourcePlacement obj)
    {
        onVideoReward?.Invoke();
        Debug.Log("reward");
    }

    public void ShowRewardedVideo()
    {
        if (IronSource.Agent.isRewardedVideoAvailable())
        {
            IronSource.Agent.showRewardedVideo();
        }
    }
}
