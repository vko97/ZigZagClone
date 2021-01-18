using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    public PlayerData()
    {
        OnFirstAppRun();
    }

    public PlayerInfo info { get; private set; }

    public PlayerInfo GetInfo()
    {
        return info;
    }

    public void OnFirstAppRun()
    {
        info = new PlayerInfo("id6", 5, false, 0);
    }
}
