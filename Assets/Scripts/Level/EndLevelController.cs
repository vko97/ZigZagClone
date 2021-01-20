using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndLevelController : MonoBehaviour
{
    [SerializeField]
    private Button nextLevelButton;
    [SerializeField]
    private TMP_Text nextLevelText;
    [SerializeField]
    private TMP_Text levelResultText;

    private void Awake()
    {
        nextLevelButton.onClick.AddListener(() => SceneManager.LoadScene(SceneManager.GetActiveScene().name));
    }

    private void ShowUI()
    {
        nextLevelButton.gameObject.SetActive(true);
        levelResultText.gameObject.SetActive(true);
    }

    public void OnLevelComplete()
    {
        ShowUI();
    }

    public void OnLevelFailed()
    {
        nextLevelText.text = "Retry";
        levelResultText.text = "You failed";
        ShowUI();
    }
}
