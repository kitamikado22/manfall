using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// 近くのオブジェクトを落下させることができる
/// </summary>
public class HoleDroppable : MonoBehaviour
{
    [SerializeField] private LayerMask obstacleLayer;

    public event Action<int> OnFall;

    // 穴に落ちたオブジェクトは削除
    private void OnTriggerExit(Collider other)
    {
        if ((obstacleLayer.value & (1 << other.gameObject.layer)) != 0)
        {
            var obstacle = other.gameObject.GetComponent<IObstacle>();
            if (obstacle != null)
            {
                OnFall?.Invoke(obstacle.Exp);
                obstacle.Die();
                return;
            }
            else Debug.Log("Error Obstacle");
        }
    }
}
