using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

/// <summary>
/// シンプルなポインタのクリックに反応するボタン
/// </summary>
public class ClickableSimpleButton : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// ボタンを押して離した時に発火する処理イベント
    /// </summary>
    public event Action OnClick;

    /// <summary>
    /// ボタンを押したときの具体的な処理
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        OnClick?.Invoke();
    }

}
