using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class NewBehaviourScript : MonoBehaviour
{
    public string volumeParameter = "MasterVolume";
    public AudioMixer audioMixer;
    // Start is called before the first frame update
    void Start()
    {
        var volumeValue = PlayerPrefs.GetFloat(volumeParameter, volumeParameter == "FonVal" ? 0f : -80f);
        audioMixer.SetFloat(volumeParameter, volumeValue);
        var volumeValues = PlayerPrefs.GetFloat(volumeParameter, volumeParameter == "CardVol" ? 0f : -80f);
        audioMixer.SetFloat(volumeParameter, volumeValue);
    }
}
