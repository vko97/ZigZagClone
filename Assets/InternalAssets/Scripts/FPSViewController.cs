using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSViewController : MonoBehaviour
{
    [SerializeField]
    private TMP_Text fpsText;

    private void Start()
    {
        StartCoroutine(ShowFps());
    }

    private IEnumerator ShowFps()
    {
        while (true)
        {
            fpsText.text = ((int)(1f / Time.deltaTime)).ToString();
            yield return new WaitForSeconds(.5f);
        }
    }
}
