using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioClip[] sounds;
    private AudioSource audioSource => GetComponent<AudioSource>();
    public void PlaySound(AudioClip clip, float volume = 1f, bool destroyed = false)
    {
        audioSource.PlayOneShot(clip, volume);
    }
}
