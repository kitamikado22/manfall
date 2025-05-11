using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ホールの近くの落下オブジェクトだけ物理運動を許可する
/// </summary>
public class HoleActiveArea : MonoBehaviour
{
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField, Range(0, 1024f)] private int bufferSize = 32;
    [SerializeField] private float activeDistance = 1f;
    [SerializeField] private Vector3 upActivePosition;
    [SerializeField] private Vector3 downActivePosition;

    private Collider[] obstacleColliders;
    private HashSet<Rigidbody> beforeRigidbodies = new HashSet<Rigidbody>();

    private float startActiveDistance;
    private Vector3 startDownActivePosition;

    // 物理挙動を許可するエリアを広げる
    public void ExpandActiveArea()
    {
        activeDistance = activeDistance * ConstHole.GrowthRate;
        downActivePosition = downActivePosition * ConstHole.GrowthRate;
    }

    // 元に戻す
    public void Initialize()
    {
        activeDistance = startActiveDistance;
        downActivePosition = startDownActivePosition;
    }

    private void Awake()
    {
        // Collierリストを持つバッファを取得
        obstacleColliders = new Collider[bufferSize];

        startActiveDistance = activeDistance;
        startDownActivePosition = downActivePosition;
    }

    private void FixedUpdate()
    {
        // 指定範囲内の落下物を探索
        int count = Physics.OverlapCapsuleNonAlloc(
            transform.position + downActivePosition,
            transform.position + upActivePosition,
            activeDistance,
            obstacleColliders,
            obstacleLayer
        );

        // 今回範囲内にいる落下物を保存
        HashSet<Rigidbody> currentRigidbodies = new HashSet<Rigidbody>();

        for (int i = 0; i < count; i++)
        {
            Rigidbody rb = obstacleColliders[i].attachedRigidbody;
            if (rb != null)
            {
                currentRigidbodies.Add(rb);

                if (rb.isKinematic)
                {   // 範囲内の落下物の物理運動を許可
                    rb.isKinematic = false;
                }
            }
            else Debug.Log("Error Rigidbody");
        }

        // 前回はいたが今回はいないオブジェクトを探索
        foreach (Rigidbody rb in beforeRigidbodies)
        {
            if (rb != null && !currentRigidbodies.Contains(rb))
            {   // 範囲外に出たら物理運動を許可しない
                rb.isKinematic = true;
            }
        }

        // 前回保存用のリストの更新
        beforeRigidbodies = currentRigidbodies;
    }


# if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Vector3 point0 = transform.position + upActivePosition;
        Vector3 point1 = transform.position + downActivePosition;

        Gizmos.color = Color.white;
        Gizmos.DrawWireSphere(point0, activeDistance);
        Gizmos.DrawWireSphere(point1, activeDistance);
        Gizmos.DrawLine(point0 + transform.right * activeDistance, point1 + transform.right * activeDistance);
        Gizmos.DrawLine(point0 - transform.right * activeDistance, point1 - transform.right * activeDistance);
        Gizmos.DrawLine(point0 + transform.forward * activeDistance, point1 + transform.forward * activeDistance);
        Gizmos.DrawLine(point0 - transform.forward * activeDistance, point1 - transform.forward * activeDistance);
    }
# endif
}
