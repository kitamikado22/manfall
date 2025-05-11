using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(AudioSource))]

public class SliderEffectSE : MonoBehaviour, IPointerClickHandler
{
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        audioSource.Play();
    }

}
