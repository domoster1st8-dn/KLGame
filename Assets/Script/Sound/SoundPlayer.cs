using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public AudioClip Sword;
    public AudioClip UseItem;
    public AudioClip TakeDamage;
    public AudioSource audioSourcePlayer;
    public void OnSoundItem()
    {
        audioSourcePlayer.clip = UseItem;
        audioSourcePlayer.Play();
    }
    public void OnSoundTakeDmg()
    {
        audioSourcePlayer.clip = TakeDamage;
        audioSourcePlayer.Play();
    }
    public void OnSoundSword()
    {
        audioSourcePlayer.clip = Sword;
        audioSourcePlayer.Play();
    }
}
