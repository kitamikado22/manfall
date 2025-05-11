using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;

/// <summary>
/// TextMeshProオブジェクトが点滅する
/// </summary>
public class FlashTextMP : MonoBehaviour
{
    private TextMeshProUGUI tmp;
    private float flashDuration = 0.8f;

    void Awake()
    {
        tmp = GetComponent<TextMeshProUGUI>();
        tmp.DOFade(0f, flashDuration)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo)
            .SetLink(gameObject);
    }
}
