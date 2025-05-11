using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ホール同士は食べる食べられる
/// </summary>
public class HoleEatable : MonoBehaviour
{
    [SerializeField] private LayerMask holeLayer;
    [SerializeField] private float radius = 0.5f;

    private Collider[] targetColliders = new Collider[2];

    private HoleGrowth holeGrowth;

    /// <summary>
    /// 食べられる範囲を拡張
    /// </summary>
    public void ExpandArea()
    {
        radius = radius * ConstHole.GrowthRate;
    }

    private void Awake()
    {
        holeGrowth = GetComponent<HoleGrowth>();
    }

    private void FixedUpdate()
    {
        int count = Physics.OverlapSphereNonAlloc(
            transform.position,
            radius,
            targetColliders,
            holeLayer
        );

        // 自分自身を除いてホールが存在しない場合
        if (count <= 1) return;

        // ほかのホールと接触した場合
        for (int i = 0; i < count; i++)
        {
            var targetGrowth = targetColliders[i].gameObject.GetComponentInParent<HoleGrowth>();

            // 自分のレベルが上であれば
            if (targetGrowth != null && targetGrowth.Level < holeGrowth.Level)
            {
                var targetHole = targetColliders[i].gameObject.GetComponentInParent<IHole>();

                // 経験値を回収
                this.holeGrowth.AddExp(targetGrowth.Exp);

                // 食べる
                targetHole?.OnEaten();
            }
        }
    }

# if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
# endif
}
