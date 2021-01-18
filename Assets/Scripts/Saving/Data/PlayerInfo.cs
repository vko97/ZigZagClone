using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerInfo
{
    public string levelId;
    public int levelMultiplier;
    public bool adsRemoved = false;
    public int bestScore = 0;

    public PlayerInfo(string levelId, int levelMultiplier, bool adsRemoved, int bestScore)
    {
        if (levelMultiplier < 1)
        {
            levelMultiplier = 1;
        }
        this.levelId = levelId;
        this.levelMultiplier = levelMultiplier;
        this.adsRemoved = adsRemoved;
        this.bestScore = bestScore;
    }

    public PlayerInfo()
    {

    }
}
