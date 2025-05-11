using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour, IObstacle
{
    [SerializeField] private int _exp;

    public int Exp
    {
        get {  return _exp; }
        private set { _exp = value; }
    }

    private AudioSource audioSource;
    private SkinnedMeshRenderer[] renderers;
    private Rigidbody rb;
    private BoxCollider boxCollider;

    private float extraGravity = 180f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        renderers = GetComponentsInChildren<SkinnedMeshRenderer>(true);
        boxCollider = GetComponent<BoxCollider>();
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.down * extraGravity, ForceMode.Acceleration);
    }

    /// <summary>
    /// 落下物が落下して死亡するときの処理主に効果音を鳴らす
    /// </summary>
    public void Die()
    {
        // 効果音
        audioSource.Play();

        // 見た目を消す
        foreach (var render in renderers)
        {
            render.enabled = false;
        }

        // あたり判定をなくす
        boxCollider.enabled = false;

        Destroy(gameObject, 2f);
    }
}
