using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ゲーム全体の管理をする。主に機能同士の仲介役
/// </summary>
public class GameDirector : MonoBehaviour
{
    [Header("プレイヤー")]
    [SerializeField] private PlayerHole playerHole;

    [Header("ポーズまたはポーズ解除処理の媒体となるUI")]
    [SerializeField] private ClickableSimpleButton pauseButton;
    [SerializeField] private ClickableSimpleButton pauseMenuBatuButton;
    [SerializeField] private ClickableSimpleButton resumeBackground;
    [SerializeField] private ClickableSimpleButton continueButton;
    [SerializeField] private GameObject continueMenu;
    
    [Header("タイトルに戻る処理関連")]
    [SerializeField] private ClickableSimpleButton titleYesButton;

    [Header("ゲームオーバー処理関連")]
    [SerializeField] private ClickableSimpleButton gameOverTitleButton;

    [Header("チュートリアル処理関連")]
    [SerializeField] private TutorialManager tutorialManager;

    private PauseManager pauseManager;
    private ScreenTransition screenTransition;
    private TimeManager timeManager;
    private GameOverManager gameOverManager;
    private GameClearManager gameClearManager;
    private PointerPosition pointerPosition;

    private void Awake()
    {
        pauseManager = GetComponent<PauseManager>();
        screenTransition = GetComponent<ScreenTransition>();
        timeManager = GetComponent<TimeManager>();
        gameOverManager = GetComponent<GameOverManager>();
        gameClearManager = GetComponent<GameClearManager>();
        pointerPosition = GetComponent<PointerPosition>();

        //Application.targetFrameRate = 60;   // FPSを60に固定
    }

    private void OnEnable()
    {
        // ポーズ処理関連
        playerHole.OnDie += EnableContinueMenu;
        playerHole.OnDie += pauseManager.Pause;
        pauseButton.OnClick += pauseManager.Pause;
        pauseMenuBatuButton.OnClick += pauseManager.Resume;
        resumeBackground.OnClick += pauseManager.Resume;
        continueButton.OnClick += pauseManager.Resume;

        // タイトル画面に戻る処理関連
        titleYesButton.OnClick += PlayTransition;
        screenTransition.OnCompleted += SwitchScene;
        screenTransition.OnCompleted += pauseManager.Resume;

        // ゲームオーバー処理関連
        timeManager.OnTimeUp += gameOverManager.NoticeGameOver;
        timeManager.OnTimeUp += pauseManager.Pause;
        gameOverTitleButton.OnClick += PlayTransition;

        // ゲームクリア処理関連
        gameClearManager.OnDestroyBoss += pauseManager.Pause;
        gameClearManager.OnDestroyBoss += PlayTransitionCenter;

        // チュートリアル
        tutorialManager.OnCompleted += timeManager.Resume;
    }
    private void OnDisable()
    {
        // ポーズ処理関連
        playerHole.OnDie -= EnableContinueMenu;
        playerHole.OnDie -= pauseManager.Pause;
        pauseButton.OnClick -= pauseManager.Pause;
        pauseMenuBatuButton.OnClick -= pauseManager.Resume;
        resumeBackground.OnClick -= pauseManager.Resume;
        continueButton.OnClick -= pauseManager.Resume;

        // タイトル画面に戻る処理関連
        titleYesButton.OnClick -= PlayTransition;
        screenTransition.OnCompleted -= SwitchScene;
        screenTransition.OnCompleted -= pauseManager.Resume;

        // ゲームオーバー処理関連
        timeManager.OnTimeUp -= gameOverManager.NoticeGameOver;
        timeManager.OnTimeUp -= pauseManager.Pause;
        gameOverTitleButton.OnClick -= PlayTransition;

        // ゲームクリア処理関連
        gameClearManager.OnDestroyBoss -= pauseManager.Pause;
        gameClearManager.OnDestroyBoss -= PlayTransitionCenter;

        // チュートリアル
        tutorialManager.OnCompleted -= timeManager.Resume;
    }

    private void PlayTransition()
    {
        screenTransition.Play(pointerPosition.ToUIPosition);
    }
    private void PlayTransitionCenter()
    {
        screenTransition.Play(Vector2.zero);
    }

    // シーンを切り替える
    private void SwitchScene()
    {
        // ゲームクリアしているなら
        if (gameClearManager.IsClearing)
        {
            SceneManager.LoadScene("EndScene");
            return;
        }

        // タイトルに戻る
        SceneManager.LoadScene("StartScene");
    }

    // コンティニュー画面を表示
    private void EnableContinueMenu()
    {
        continueMenu.SetActive(true);
    }
}
