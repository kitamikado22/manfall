using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ゲームオーバーに関する処理を管理
/// </summary>
public class GameOverManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;

    /// <summary>
    /// ゲームオーバーであることをプレイヤーに通告
    /// </summary>
    public void NoticeGameOver()
    {
        gameOverPanel.SetActive(true);
    }
}
