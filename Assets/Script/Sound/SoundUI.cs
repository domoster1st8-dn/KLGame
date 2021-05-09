using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundUI : MonoBehaviour
{
    public AudioClip SoundClipUI;
    public AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = SoundClipUI;
        
    }

    public void OnSound()
    {
        audioSource.Play();
    }
}
