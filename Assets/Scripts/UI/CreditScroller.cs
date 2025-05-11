using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// エンディングでクレジットを自動でスクロール
/// </summary>
public class CreditScroller : MonoBehaviour
{
    [SerializeField] private RectTransform contentRectTransform;

    private const float scrollSpeed = 50f;

    private void Update()
    {
        contentRectTransform.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
    }
}
