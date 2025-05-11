using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ClickableSimpleButton))]

/// <summary>
/// ボタンを押されたときに指定のUIを有効にすることができる
/// </summary>
public class OpenUI : MonoBehaviour
{
    [SerializeField] private List<GameObject> uiTargets;

    private ClickableSimpleButton clickableSimpleButton;

    private void Awake()
    {
        clickableSimpleButton = GetComponent<ClickableSimpleButton>();
    }
    private void OnEnable()
    {
        clickableSimpleButton.OnClick += Open;
    }
    private void OnDisable()
    {
        clickableSimpleButton.OnClick -= Open;
    }

    private void Open()
    {
        foreach (var target in uiTargets)
        {
            target.SetActive(true);
        }
    }
}
