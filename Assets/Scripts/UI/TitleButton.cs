using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// タイトルに戻るボタン
/// </summary>
public class TitleButton : MonoBehaviour
{
    private ClickableSimpleButton clickableSimpleButton;

    private void Awake()
    {
        clickableSimpleButton = GetComponent<ClickableSimpleButton>();
    }

    private void OnEnable()
    {
        clickableSimpleButton.OnClick += BackToTitle;
    }
    private void OnDisable()
    {
        clickableSimpleButton.OnClick -= BackToTitle;
    }

    private void BackToTitle()
    {
        SceneManager.LoadScene("StartScene");
    }
}
