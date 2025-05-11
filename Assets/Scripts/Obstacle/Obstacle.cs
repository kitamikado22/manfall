using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshCollider))]
[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(ObstacleSEManager))]

/// <summary>
/// 落下物クラス
/// </summary>
public class Obstacle : MonoBehaviour, IObstacle
{
    [SerializeField] private int _exp;

    public int Exp
    {
        get {  return _exp; }
        private set { _exp = value; }
    }

    private AudioSource audioSource;
    private MeshRenderer meshRenderer;
    private MeshCollider meshCollider;
    private ObstacleSEManager seManager;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        meshRenderer = GetComponent<MeshRenderer>();
        meshCollider = GetComponent<MeshCollider>();
        seManager = GetComponent<ObstacleSEManager>();
    }

    /// <summary>
    /// 落下物が落下して死亡するときの処理主に効果音を鳴らす
    /// </summary>
    public void Die()
    {
        // 効果音
        seManager.Play(audioSource);

        // 見た目を消す
        meshRenderer.enabled = false;

        // あたり判定をなくす
        meshCollider.enabled = false;

        // オブジェクトを削除
        Destroy(gameObject, audioSource.clip.length);
    }

    //private Rigidbody rb;
    //private MeshCollider meshCollider;

    //private void Awake()
    //{
    //    rb = GetComponent<Rigidbody>();
    //    meshCollider = GetComponent<MeshCollider>();

    //    meshCollider.convex = true;
    //    rb.isKinematic = true;  // 物理運動を許可しない
    //}
}
