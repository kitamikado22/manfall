using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

/// <summary>
/// オーディオの音量の管理
/// </summary>
public class AudioVolumeManager : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider bgmSlider;
    [SerializeField] private Slider seSlider;

    private void Awake()
    {
        audioMixer.GetFloat("BGM", out float bgmVolume);
        bgmSlider.value = bgmVolume;

        audioMixer.GetFloat("SE", out float seVolume);
        seSlider.value = seVolume;

        //DontDestroyOnLoad(gameObject);
    }

    public void SetBGM(float volume)
    {
        audioMixer.SetFloat("BGM", volume);
    }
    public void SetSE(float volume)
    {
        audioMixer.SetFloat("SE", volume);
    }
}
