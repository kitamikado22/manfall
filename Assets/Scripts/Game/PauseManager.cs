using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ポーズに関する処理を管理。時間やプレイヤーの入力などすべて
/// </summary>
public class PauseManager : MonoBehaviour
{
    [SerializeField] private DynamicJoystick playerInput;
    [Header("ポーズする必要があるもの"), SerializeField]
    private List<GameObject> pausableObjects;

    private List<IPausable> pausables = new List<IPausable>();

    private void Awake()
    {
        for (int i = 0; i < pausableObjects.Count; i++)
        {
            IPausable pausable = pausableObjects[i].GetComponent<IPausable>();
            if (pausable != null)
            {
                pausables.Add(pausable);
            } else Debug.Log("nullError : pausable");
        }
    }

    /// <summary>
    /// ポーズ
    /// </summary>
    public void Pause()
    {
        // プレイヤーの入力手段をシャットダウン
        playerInput.gameObject.SetActive(false);

        foreach (var pausable in pausables)
        {
            pausable.Pause();
        }

        // 物理運動を停止
        //Physics.simulationMode = SimulationMode.Script;
    }
    /// <summary>
    /// ポーズ解除
    /// </summary>
    public void Resume()
    {
        playerInput.gameObject.SetActive(true);

        foreach (var pausable in pausables)
        {
            pausable.Resume();
        }

        // 物理運動を再開
        //Physics.simulationMode = SimulationMode.FixedUpdate;
    }
}
