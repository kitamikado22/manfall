using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ホールのあたり判定を管理
/// </summary>
public class HoleCollider : MonoBehaviour
{
    [SerializeField] private MeshCollider generatedMeshCollider;
    [SerializeField] private PolygonCollider2D ground2DCollider;
    [SerializeField] private Transform[] holeTransforms;    // 登録されているホールの数はholeTransformsに合わせる
    [SerializeField] private PolygonCollider2D[] hole2DColliders;

    private Mesh generatedMesh;

    private void Awake()
    {
        // ホールの数だけPathを登録
        ground2DCollider.pathCount += holeTransforms.Length;

        for (int i = 0; i < holeTransforms.Length; i++)
        {
            MakeHole2D(i);
        }
        
        Make3DMeshCollider();
    }

    private void FixedUpdate()
    {
        for (int i = 0; i < holeTransforms.Length; i++)
        {
            if (holeTransforms[i].hasChanged)
            {   // 座標、スケールの変更があった時だけ
                holeTransforms[i].hasChanged = false;
                MakeHole2D(i);
            }
        }

        Make3DMeshCollider();
    }


    /// <summary>
    /// 地面のあたり判定からホールのあたり判定でくり抜く
    /// </summary>
    private void MakeHole2D(int index)
    {
        Vector2[] pointPositions = hole2DColliders[index].GetPath(0);

        float scaleMultiplier = hole2DColliders[index].transform.localScale.x * holeTransforms[index].transform.localScale.x;

        for (int i = 0; i < pointPositions.Length; i++)
        {   
            // スケールを考慮して頂点座標を調整
            pointPositions[i] = scaleMultiplier * pointPositions[i];
            
            // ワールド座標に変換、2D上での座標を合わせる
            pointPositions[i] += new Vector2(holeTransforms[index].position.x, holeTransforms[index].position.z);
        }

        ground2DCollider.SetPath(index + 1, pointPositions);    // 地面のあたり判定にホールを追加
    }

    /// <summary>
    /// 2Dの地面のあたり判定を3Dに変換
    /// </summary>
    private void Make3DMeshCollider()
    {
        // 過去のメッシュデータを破棄
        if (generatedMesh != null) Destroy(generatedMesh);

        // 2Dのポリゴンを3Dのメッシュに変換
        generatedMesh = ground2DCollider.CreateMesh(true, true);

        // 作成したメッシュを反映
        generatedMeshCollider.sharedMesh = generatedMesh;
    }
}
