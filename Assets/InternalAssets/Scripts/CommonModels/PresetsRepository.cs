﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(menuName = "Models/Presets repository")]
public class PresetsRepository : ScriptableObject
{
    [SerializeField]
    private List<Preset> presets = new List<Preset>();

    public List<Preset> GetPresets()
    {
        return presets;
    }

    public int GetPresetNumber(string id)
    {
        var preset = presets.Where(item => item.id == id).FirstOrDefault();
        return presets.IndexOf(preset);
    }
}

[System.Serializable]
public class Preset
{
    public string id;
    public Color platformColor;
    [Range(20, 70)]
    public int platformNumber;
}
