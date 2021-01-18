using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ZigZag;

public static class Utils
{
    public static float OpositeDirectionSpawnChance(int multiplier)
    {
        float chance = Constants.basicOpositeDirSpawnChance + 0.1f * (multiplier - 1);
        return Mathf.Clamp(chance, 0.3f, 0.9f);
    }

    public static float CrystalSpawnChance(int multiplier)
    {
        float chance = Constants.basicCrystalSpawnChance + 0.1f * (multiplier - 1);
        return Mathf.Clamp(chance, 0.1f, 0.7f);
    }

    public static int PlatformNumber(int basicNumber, int multiplier)
    {
        return basicNumber * multiplier;
    }

    public static float MovementSpeed(int multiplier)
    {
        return Constants.basicMoveSpeed * (1 + ((multiplier - 1) * 0.1f));
    }
}
