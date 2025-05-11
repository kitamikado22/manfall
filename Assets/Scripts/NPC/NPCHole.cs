using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// NPCのホールのすべての機能を管理する
/// </summary>
public class NPCHole : MonoBehaviour, IHole
{
    private NPCMoveable npcMoveable;
    private HoleDroppable holeDroppable;
    private HoleGrowth holeGrowth;
    private HoleActiveArea holeActiveArea;

    private void Awake()
    {
        npcMoveable = GetComponent<NPCMoveable>();
        holeDroppable = GetComponent<HoleDroppable>();
        holeGrowth = GetComponent<HoleGrowth>();
        holeActiveArea = GetComponent<HoleActiveArea>();
    }
    private void OnEnable()
    {
        holeGrowth.OnUpdateSpeed += npcMoveable.SetSpeed;
        holeDroppable.OnFall += holeGrowth.AddExp;
        holeGrowth.OnLevelUp += holeActiveArea.ExpandActiveArea;
    }
    private void OnDisable()
    {
        holeGrowth.OnUpdateSpeed -= npcMoveable.SetSpeed;
        holeDroppable.OnFall -= holeGrowth.AddExp;
        holeGrowth.OnLevelUp -= holeActiveArea.ExpandActiveArea;
    }

    /// <summary>
    /// 他のホールから食べられた時の処理
    /// </summary>
    public void OnEaten()
    {
        gameObject.SetActive(false);
    }
}
