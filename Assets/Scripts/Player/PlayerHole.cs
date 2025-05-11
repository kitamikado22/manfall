using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(PlayerMoveable))]
[RequireComponent(typeof(PlayerCameraController))]
[RequireComponent(typeof(HoleDroppable))]
[RequireComponent(typeof(HoleGrowth))]
[RequireComponent(typeof(HoleActiveArea))]

/// <summary>
/// プレイヤーのホールのすべての機能を管理する
/// </summary>
public class PlayerHole : MonoBehaviour, IHole
{
    public event Action OnDie;

    private PlayerMoveable playerMoveable;
    private PlayerCameraController playerCameraController;
    private HoleDroppable holeDroppable;
    private HoleGrowth holeGrowth;
    private HoleActiveArea holeActiveArea;
    private HoleEatable holeEatable;
    private Rigidbody rb;

    private Vector3 startPosition;
    private Vector3 startScale;

    private void Awake()
    {
        playerMoveable = GetComponent<PlayerMoveable>();
        playerCameraController = GetComponent<PlayerCameraController>();
        holeDroppable = GetComponent<HoleDroppable>();
        holeGrowth = GetComponent<HoleGrowth>();
        holeActiveArea = GetComponent<HoleActiveArea>();
        holeEatable = GetComponent<HoleEatable>();
        rb = GetComponent<Rigidbody>();

        startPosition = transform.position;
        startScale = transform.localScale;
    }

    private void OnEnable()
    {
        holeDroppable.OnFall += holeGrowth.AddExp;
        holeGrowth.OnUpdateSpeed += playerMoveable.SetSpeed;
        holeGrowth.OnLevelUp += playerCameraController.SetCameraDistance;
        holeGrowth.OnLevelUp += playerCameraController.SetOrthographicSize;
        holeGrowth.OnLevelUp += holeActiveArea.ExpandActiveArea;
        holeGrowth.OnLevelUp += holeEatable.ExpandArea;
    }
    private void OnDisable()
    {
        holeDroppable.OnFall -= holeGrowth.AddExp;
        holeGrowth.OnUpdateSpeed -= playerMoveable.SetSpeed;
        holeGrowth.OnLevelUp -= playerCameraController.SetCameraDistance;
        holeGrowth.OnLevelUp -= playerCameraController.SetOrthographicSize;
        holeGrowth.OnLevelUp -= holeActiveArea.ExpandActiveArea;
        holeGrowth.OnLevelUp -= holeEatable.ExpandArea;
    }

    public void OnEaten()
    {
        // 初期地点にリスポーン
        rb.MovePosition(startPosition);
        transform.localScale = startScale;

        // 成長要素を初期化
        playerCameraController.Initialize();
        holeGrowth.Initialize();
        holeActiveArea.Initialize();

        Debug.Log("initialize");

        OnDie?.Invoke();
    }
}
