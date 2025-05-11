using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// ゲームクリアに関する処理を管理
/// </summary>
public class GameClearManager : MonoBehaviour
{
    [SerializeField] private GameObject bossObject;

    public event Action OnDestroyBoss;

    public bool IsClearing { get; private set; }

    private void Awake()
    {
        IsClearing = false;
    }

    private void Update()
    {
        // ボスが倒されたか監視
        if (!IsClearing && bossObject == null)
        {
            IsClearing = true;

            OnDestroyBoss?.Invoke();
        }
    }
}
