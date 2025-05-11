using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using DG.Tweening;

/// <summary>
/// 画面遷移のアニメーション演出を行ってくれる
/// </summary>
public class ScreenTransition : MonoBehaviour
{
    [SerializeField] private RectTransform circleRectTransform;

    /// <summary>
    /// アニメーション完了した時の処理
    /// </summary>
    public event Action OnCompleted;

    private const float endScale = 30f;
    private const float duration = 1f;

    /// <summary>
    /// 指定の座標を始めとするアニメーション演出開始
    /// </summary>
    public void Play(in Vector2 uiPosition)
    {
        circleRectTransform.anchoredPosition = uiPosition;

        circleRectTransform.gameObject.SetActive(true);

        // アニメーション
        circleRectTransform.DOScale(endScale, duration)
            .SetLink(circleRectTransform.gameObject)
            .SetEase(Ease.InCubic)
            .OnComplete(() => OnCompleted?.Invoke());
    }
}
