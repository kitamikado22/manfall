using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[RequireComponent(typeof(Rigidbody))]

/// <summary>
/// プレイヤーをジョイスティックで動かせる
/// </summary>
public class PlayerMoveable : MonoBehaviour, IPausable
{
    [SerializeField] private DynamicJoystick dynamicJoystick;
    [SerializeField] private Transform triangleDirectionTransform;
    [SerializeField] private Transform cameraTransform;
    private Rigidbody rb;
    private float speed = ConstHole.initialSpeed;

    private Vector3 cameraForward;
    private Vector3 cameraRight;

    private bool isPaused = false;

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    public void Pause()
    {
        rb.velocity = Vector3.zero;
        isPaused = true;
    }
    public void Resume()
    {
        isPaused = false;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // カメラの方向で、x-z平面の単位ベクトルを取得
        cameraForward = Vector3.Scale(cameraTransform.forward, new Vector3(1,0,1)).normalized;
        cameraRight = Vector3.Scale(cameraTransform.right, new Vector3(1,0,1)).normalized;
    }

    private void FixedUpdate()
    {
        if (isPaused) return;

        // カメラ基準の移動方向ベクトルを計算
        Vector3 moveDirection = cameraForward * dynamicJoystick.Vertical + cameraRight * dynamicJoystick.Horizontal;
        rb.velocity = speed * moveDirection;

        // 入力があるときに回転系更新
        if (moveDirection.sqrMagnitude > 0.01f)
        {
            triangleDirectionTransform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }
}
