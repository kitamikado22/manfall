using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ポインタの座標を監視して保存してくれる
/// </summary>
public class PointerPosition : MonoBehaviour
{
    [SerializeField] private Canvas canvas;

    /// <summary>
    /// ポインターの座標
    /// </summary>
    public Vector2 Value { get; private set; }

    /// <summary>
    /// UIのRectTransformタイプの座標系に変換したもの
    /// </summary>
    public Vector2 ToUIPosition
    {
        get {
            Vector2 uiPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(
                canvas.transform as RectTransform,
                Value,
                canvas.worldCamera,
                out uiPosition
            );
            return uiPosition;
        }
        private set { }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.touchCount > 0)
        {
            // ポインターの座標を保存
            Value = (Vector2)Input.mousePosition;
            if (Input.touchCount > 0)
            {
                Value = Input.GetTouch(0).position;
            }
        }
    }
}
