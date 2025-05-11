using System;
using DG.Tweening;
using DG.Tweening.Core;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// DOTweenの機能拡張
/// </summary>
public static class DOTweenExtensions
{
    /// <summary>
    /// TMProでフォントのサイズを変えられる
    /// </summary>
    /// <param name="target">サイズを変更させる対象</param>
    /// <param name="startSize">フォントサイズ初期値</param>
    /// <param name="endSize">目標のフォントサイズ</param>
    /// <param name="duration">かかる時間</param>
    public static Tween DOTextSize(this TMP_Text target, float startSize, float endSize, float duration)
    {
        float currentSize = startSize;
        //target.fontSize = startSize;

        return DOTween.To(() => currentSize, value =>
        {
            currentSize = value;
            target.fontSize = value;
        }, endSize, duration);
    }
}
