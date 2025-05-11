using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// チュートリアルに関する処理を管理
/// </summary>
public class TutorialManager : MonoBehaviour
{
    [SerializeField] private DynamicJoystick dynamicJoystick;

    public event Action OnCompleted;

    private void Update()
    {
        if (0f < dynamicJoystick.Vertical || 0f < dynamicJoystick.Horizontal)
        {
            gameObject.SetActive(false);
            OnCompleted?.Invoke();
        }
    }
}
