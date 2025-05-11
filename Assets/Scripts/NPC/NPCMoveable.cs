using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NPCがランダムに動く
/// </summary>
public class NPCMoveable : MonoBehaviour, IPausable
{
    [SerializeField] private Transform triangleDirectionTransform;

    private Rigidbody rb;
    private float speed = ConstHole.initialSpeed;
    private bool isPaused = false;

    private Vector3 moveDirection;
    private float moveTimer = 0;

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
    }

    private void FixedUpdate()
    {
        if (isPaused) return;

        if (moveTimer > 0)
        {
            // 移動
            rb.velocity = speed * moveDirection;
            // 回転
            triangleDirectionTransform.rotation = Quaternion.LookRotation(moveDirection);

            moveTimer -= Time.deltaTime;
        }
        else
        {
            // 移動する方向、時間を決定
            DecideMoving();
        }

    }

    private void DecideMoving()
    {
        // 移動する方向を決める
        moveDirection = Random.insideUnitSphere.normalized;
        moveDirection = new Vector3(moveDirection.x, 0f, moveDirection.y);
        // 移動する時間を決める
        moveTimer = Random.Range(1f, 3f);
    }
}
