using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 落下物のSEに関する管理
/// </summary>
public class ObstacleSEManager : MonoBehaviour
{
    private int maxPlaySounds = 32;
    private static List<AudioSource> activeSorces = new List<AudioSource>();

    public void Play(AudioSource audioSource)
    {
        // 再生していないものはリストから除外
        activeSorces.RemoveAll(src => src == null || !src.isPlaying);

        // 同時に再生できる数に限度がある
        if (activeSorces.Count > maxPlaySounds)
        {
            //Debug.Log("Limit");
            return;
        }

        audioSource.Play();
        activeSorces.Add(audioSource);
        //Debug.Log("count : " + activeSorces.Count);
    }
}
