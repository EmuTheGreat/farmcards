using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioButton : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clickSource;
    public void ClickSound()
    {
        audioSource.PlayOneShot(clickSource);
    }
}
