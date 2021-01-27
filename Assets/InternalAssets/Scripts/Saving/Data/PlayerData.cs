using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZigZag.Firebase;

[System.Serializable]
public class PlayerData
{
    #region Singleton
    private static PlayerData instance;

    public static PlayerData Instance()
    {
        if (instance == null)
        {
            instance = new PlayerData();
        }
        return instance;
    }
    #endregion

    public delegate void OnLoadedHandler();
    public event OnLoadedHandler onLoadedData;

    public delegate void OnAdsRemovedHandler();
    public event OnAdsRemovedHandler onAdsRemove;

    public PlayerInfo info { get; private set; }

    public void OnFirstAppRun()
    {
        info = new PlayerInfo("id1", 1, false, 0);
    }

    public async void LoadData()
    {
        info =  await FirebaseController.Instance().Load();
        if (info == null)
        {
            OnFirstAppRun();
            SaveData();
        }
        onLoadedData?.Invoke();
    }

    public void SaveData()
    {
        FirebaseController.Instance().Save(info);
    }

    public void RemoveAds()
    {
        info.adsRemoved = true;
        onAdsRemove?.Invoke();
        SaveData();
    }
}