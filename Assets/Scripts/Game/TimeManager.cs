using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

/// <summary>
/// 時間制限について管理
/// </summary>
public class TimeManager : MonoBehaviour, IPausable
{
    [SerializeField] private TextMeshProUGUI timeText;
    private const float timeLimit = 180f;
    private float currentTime;
    private bool isRunning = true;
    private bool isPausing = true;

    /// <summary>
    /// 残り時間が無くなった時に発火
    /// </summary>
    public event Action OnTimeUp;

    public void Pause()
    {
        isPausing = true;
    }
    public void Resume()
    {
        isPausing = false;
    }

    private void Awake()
    {
        currentTime = timeLimit;
    }

    private void Update()
    {
        // ポーズ中または稼働していない場合は処理なし
        if (isPausing || !isRunning) return;

        currentTime -= Time.deltaTime;
        if (currentTime <= 0)
        {
            currentTime = 0f;
            isRunning = false;
            OnTimeUp?.Invoke();
        }

        UpdateScreen();
    }

    /// <summary>
    /// UI、画面を更新
    /// </summary>
    private void UpdateScreen()
    {
        int seconds = Mathf.CeilToInt(currentTime);
        int minutes = seconds / 60;
        seconds = seconds - 60 * minutes;

        timeText.SetText(minutes + ":" + seconds.ToString("D2"));
    }
}
