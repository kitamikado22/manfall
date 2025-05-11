using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;

/// <summary>
/// ホールの成長要素
/// </summary>
public class HoleGrowth : MonoBehaviour
{
    [SerializeField] private Image ringMask;
    [SerializeField] private TextMeshProUGUI textLevel;

    public event Action<float> OnUpdateSpeed;
    public event Action OnUpdateVisual;
    public event Action OnLevelUp;

    private BoxCollider droppableCollider;
    private float fixedCenterY;
    private float fixedSizeY;

    private float scale;
    private float _speed;
    public float Speed
    {
        get { return _speed; }
        private set {
            _speed = value;
            OnUpdateSpeed?.Invoke(value);
        }
    }
    private int _level;
    public int Level
    {
        get { return _level; }
        private set {
            if (ConstHole.limitLevel < value)
            {
                _level = ConstHole.limitLevel;
            }
            else _level = value;
        }
    }
    public int Exp { get; private set; }



    private void Awake()
    {
        droppableCollider = GetComponent<BoxCollider>();
        fixedCenterY = droppableCollider.center.y;
        fixedSizeY = droppableCollider.size.y;

        Initialize();
    }

    /// <summary>
    /// 初期化処理
    /// </summary>
    public void Initialize()
    {
        Level = 1;
        Exp = 0;
        _speed = ConstHole.initialSpeed;
        scale = transform.localScale.x;

        UpdateVisual(true);
    }
    /// <summary>
    /// 経験値を増やしてレベルが上がったり成長したりする
    /// </summary>
    public void AddExp(int exp)
    {

        Exp += exp;

        bool didLevelUp = false;

        // レベルアップ処理、一度に二段階以上、上がる場合も考慮
        while (true)
        {
            // 限界レベルに達した場合
            if (ConstHole.limitLevel <= Level) break;

            // 必要経験値を持っている経験値が上回ったらレベル上げ
            if (levelDatas[Level].requiredExp <= Exp)
            {
                didLevelUp = true;
                LevelUp();
            }
            else break;
        }

        // 見た目の更新
        UpdateVisual(didLevelUp);
    }
    /// <summary>
    /// 穴のレベルアップ処理、成長する
    /// </summary>
    private void LevelUp()
    {
        // 成長
        Level = Level + 1;
        scale = scale * ConstHole.GrowthRate;
        Speed = Speed * ConstHole.GrowthRate;

        // 落下判定をするコライダーは高さとサイズは変更したくない
        droppableCollider.center = new Vector3(droppableCollider.center.x, fixedCenterY / transform.localScale.y, droppableCollider.center.z);
        droppableCollider.size = new Vector3(droppableCollider.size.x, fixedSizeY / transform.localScale.y, droppableCollider.size.z);

        OnLevelUp?.Invoke();
    }
    /// <summary>
    /// 穴の見た目の更新、EXPリング、レベル表記など
    /// </summary>
    /// <param name="didLevelUp">レベルが上がったか</param>
    private void UpdateVisual(bool didLevelUp)
    {
        // EXPリングの更新
        int currentExp = Exp - levelDatas[Level-1].requiredExp;
        int nextRequireExp = levelDatas[Level].requiredExp - levelDatas[Level-1].requiredExp;
        ringMask.fillAmount = 1f - Math.Clamp((float)currentExp / nextRequireExp, 0f, 1f);

        // レベル表記の更新
        if (didLevelUp)
        {
            textLevel.SetText("Lv." + Level);
        }

        // 実際のスケールを更新
        transform.localScale = new Vector3(scale, scale, scale);

        OnUpdateVisual?.Invoke();
    }

    /// <summary>
    /// レベルアップに必要な経験値データ
    /// </summary>
    private struct LevelData
    {
        public int level;
        public int requiredExp;

        public LevelData(int level, int requiredExp)
        {
            this.level = level;
            this.requiredExp = requiredExp;
        }
    }
    /// <summary>
    /// 必要経験値テーブル
    /// </summary>
    private readonly LevelData[] levelDatas = new LevelData[] {
        new LevelData(1, 0),
        new LevelData(2, 5),
        new LevelData(3, 12),
        new LevelData(4, 24),
        new LevelData(5, 48),
        new LevelData(6, 88),
        new LevelData(7, 152),
        new LevelData(8, 252),
        new LevelData(9, 412),
        new LevelData(10, 732),
        new LevelData(11, 1372),
        new LevelData(12, 2552),
        new LevelData(13, 4912),
        new LevelData(14, 9532),
        new LevelData(15, 18772),
        new LevelData(16, 18773)    // 存在しない
    };
}
