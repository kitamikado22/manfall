using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera virtualCamera;
    private CinemachineFramingTransposer framingTransposer;

    private const float scaleUpDuration = 0.8f;
    private const float limitCameraDistance = 500f;
    private const float limitOrthographicSize = 500f;

    private float targetCameraDistance;
    private float targetOrthographicSize;
    private float startCameraDistance;
    private float startOrthographicSize;

    private void Awake()
    {
        framingTransposer = virtualCamera.GetCinemachineComponent<CinemachineFramingTransposer>();

        targetCameraDistance = startCameraDistance = framingTransposer.m_CameraDistance;
        targetOrthographicSize = startOrthographicSize = virtualCamera.m_Lens.OrthographicSize;
    }

    /// <summary>
    /// カメラとの距離を調整
    /// </summary>
    public void SetCameraDistance()
    {
        // 目標となる距離を計算
        targetCameraDistance = targetCameraDistance * ConstHole.GrowthRate;

        // 限界を設定
        if (limitCameraDistance <= targetCameraDistance)
            targetCameraDistance = limitCameraDistance;

        float currentDistance = framingTransposer.m_CameraDistance;
        DOTween.To(() => currentDistance, value =>
        {
            currentDistance = value;
            framingTransposer.m_CameraDistance = value;
        }, targetCameraDistance, scaleUpDuration)
            .SetEase(Ease.InOutQuad);
    }
    /// <summary>
    /// カメラのレンズサイズをスケールから調整
    /// </summary>
    public void SetOrthographicSize()
    {
        // 目標となるサイズを調整
        targetOrthographicSize = targetOrthographicSize * ConstHole.GrowthRate;

        // 限界を設定
        if (limitOrthographicSize <= targetOrthographicSize)
            targetOrthographicSize = limitOrthographicSize;

        float currentSize = virtualCamera.m_Lens.OrthographicSize;
        DOTween.To(() => currentSize, value =>
        {
            currentSize = value;
            virtualCamera.m_Lens.OrthographicSize = value;
        }, targetOrthographicSize, scaleUpDuration)
            .SetEase(Ease.InOutQuad);
    }

    /// <summary>
    /// カメラとの距離、カメラレンズのサイズを初期化
    /// </summary>
    public void Initialize()
    {
        targetCameraDistance = startCameraDistance;
        targetOrthographicSize = startOrthographicSize;
    }
}
