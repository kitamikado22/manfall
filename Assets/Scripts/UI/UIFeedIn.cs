using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;


/// <summary>
/// UIオブジェクトの有効化をトリガーにフェードイン演出
/// </summary>
[RequireComponent(typeof(RectTransform))]
[RequireComponent(typeof(CanvasGroup))]

public class UIFeedIn : MonoBehaviour
{
    // どこからフェードインするか
    private enum From
    {
        Right,
        Left,
        Top,
        Bottom
    };
    // フェードイン定数
    private const float distanceFeedin = 50f;
    private const float durationFeedin = 0.8f;
    // フェードインする際のずれる距離のテーブル
    private static readonly Vector2[] distance = new Vector2[4] {
        new Vector2(distanceFeedin, 0),
        new Vector2(-distanceFeedin, 0),
        new Vector2(0, distanceFeedin),
        new Vector2(0, -distanceFeedin)
    };

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    [Header("どの方向からフェードインするか選択")]
    [SerializeField] private From from;

    // プロパティ
    public Sequence Sequence { get; private set; }

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();

        Sequence = DOTween.Sequence().SetLink(gameObject);
    }

    private void OnEnable()
    {
        // 演出準備
        Vector2 targetPosition = rectTransform.anchoredPosition;
        rectTransform.anchoredPosition = rectTransform.anchoredPosition + distance[(int)from];
        canvasGroup.alpha = 0f;

        // アニメーション演出
        Sequence.Append(rectTransform.DOAnchorPos(targetPosition, durationFeedin).SetEase(Ease.OutQuart));
        Sequence.Join(canvasGroup.DOFade(1f, durationFeedin).SetEase(Ease.Linear));
    }
}
