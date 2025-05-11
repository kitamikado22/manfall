using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]

/// <summary>
/// 効果音付きUIボタン
/// </summary>
public class ButtonEffectSE : MonoBehaviour, IPointerDownHandler
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        audioSource.Play();
    }
}
