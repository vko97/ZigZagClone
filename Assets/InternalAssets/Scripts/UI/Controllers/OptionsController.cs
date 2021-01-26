using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField input;
    [SerializeField]
    private TMP_Text placeholder;
    [SerializeField]
    private Button setNameButton;
    [SerializeField]
    private Button backButton;

    private void Awake()
    {
        setNameButton.onClick.AddListener(() => OnSetName());
        backButton.onClick.AddListener(() => gameObject.SetActive(false));
    }

    private void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        var info = PlayerData.Instance().info;
        placeholder.text = info.name;
        input.text = null;
    }

    private void OnSetName()
    {
        PlayerData.Instance().info.name = input.text;
        Initialize();
        PlayerData.Instance().SaveData();
    }
}
