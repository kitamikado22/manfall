using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 画面をタッチするとゲームが始まる
/// </summary>
public class GameStarter : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreen;
    [SerializeField] private ClickableSimpleButton touchToScreen; 

    private PointerPosition pointerPosition;
    private ScreenTransition screenTransition;
    private SplitLoader splitLoader;
    private bool isLoading = false;

    private void Awake()
    {
        screenTransition = GetComponent<ScreenTransition>();
        pointerPosition = GetComponent<PointerPosition>();
        splitLoader = GetComponent<SplitLoader>();
    }
    private void OnEnable()
    {
        screenTransition.OnCompleted += StartGame;
        touchToScreen.OnClick += StartTransition;
    }
    private void OnDisable()
    {
        screenTransition.OnCompleted -= StartGame;
        touchToScreen.OnClick -= StartTransition;
    }

    // アニメーション演出を開始
    private void StartTransition()
    {
        if (isLoading) return;
        
        isLoading = true;

        screenTransition.Play(pointerPosition.ToUIPosition);
    }
    // 演出が終わってゲームを開始
    private void StartGame()
    {
        // ロード画面を表示
        loadingScreen.SetActive(true);

        // シーンをロード
        splitLoader.StartLoading("MainScene");
    }
}
