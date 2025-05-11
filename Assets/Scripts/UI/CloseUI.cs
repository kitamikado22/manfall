using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ClickableSimpleButton))]

/// <summary>
/// クリックされて指定のUIを閉じることができる
/// </summary>
public class CloseUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> uiTargets;

    private ClickableSimpleButton clickableSimpleButton;

    private void Awake()
    {
        clickableSimpleButton = GetComponent<ClickableSimpleButton>();
    }
    private void OnEnable()
    {
        clickableSimpleButton.OnClick += Close;
    }
    private void OnDisable()
    {
        clickableSimpleButton.OnClick -= Close;
    }

    private void Close()
    {
        foreach (var target in uiTargets)
        {
            target.SetActive(false);
        }
    }
}
