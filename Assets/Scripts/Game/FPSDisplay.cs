using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

/// <summary>
/// FPSを表示する
/// </summary>
public class FPSDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpsText;
    private float deltaTime = 0f;

    private void Update()
    {
        deltaTime += (Time.deltaTime - deltaTime) * 0.05f;   // 平滑化
        int fps = Mathf.CeilToInt(1.0f / deltaTime);
        fpsText.SetText("FPS:" + fps);
    }
}
