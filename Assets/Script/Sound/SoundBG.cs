using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundBG : MonoBehaviour
{
    public AudioClip BGSound;
    public AudioClip BGBoss;
    public AudioSource audioSource;
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = BGSound;
        audioSource.Play();
    }

    
    public void InBoss()
    {
        audioSource.clip = BGBoss;
        audioSource.Play();
    }
    public void OnSound()
    {
        audioSource.Play();
    }
    public void PauseSound()
    {
        audioSource.Pause();
    }
}
