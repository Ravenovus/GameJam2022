using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager audioManager;
    public AudioSource[] SoundEffects;
    public AudioSource bgm;

    private int lastSFX = 0;

    private void Awake()
    {
        audioManager = this;
    }

    public void PlaySFX(int soundToPlay)
    {
        SoundEffects[lastSFX].Stop();
        SoundEffects[soundToPlay].Play();
        lastSFX = soundToPlay;
    }

}

