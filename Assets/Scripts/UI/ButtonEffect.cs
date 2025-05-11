using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// 演出効果付きの通常ボタン
/// </summary>
public class ButtonEffect : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    /// <summary>
    /// ボタンを押して離した時に発火する処理イベント
    /// </summary>
    //public event Action OnClick;
    private Vector3 baseScale;

    // 演出のための定数
    private const float rateScaleOnDown = 0.9f;
    private const float animationDuration = 0.1f;

    private void Awake()
    {
        baseScale = transform.localScale;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOScale(baseScale * rateScaleOnDown, animationDuration)
            .SetEase(Ease.InOutSine)
            .SetLink(gameObject);
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOScale(baseScale, animationDuration)
            .SetEase(Ease.InOutSine)
            .SetLink(gameObject);
    }

    /// <summary>
    /// ボタンを押したときの具体的な処理
    /// </summary>
    /// <param name="eventData"></param>
    //public void OnPointerClick(PointerEventData eventData)
    //{
    //    OnClick?.Invoke();
    //}
}
