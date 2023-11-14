using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public string volumeParameter = "MasterVolume";
    public AudioMixer audioMixer;
    public Slider volumeSlider;
    private float _volumeValue;
    private const float _multiplier = 20f;
    private void Awake()
    {
        volumeSlider.onValueChanged.AddListener(HandSliderValueChanged);
    }
    private void HandSliderValueChanged(float value)
    {
        _volumeValue = Mathf.Log10(value) * _multiplier;
        audioMixer.SetFloat(volumeParameter, _volumeValue);
    }
    // Start is called before the first frame update
    void Start()
    {
        _volumeValue = PlayerPrefs.GetFloat(volumeParameter, Mathf.Log10(volumeSlider.value) * _multiplier);
        volumeSlider.value = Mathf.Pow(10f, _volumeValue / _multiplier);
    }
    private void OnDisable()
    {
        PlayerPrefs.SetFloat(volumeParameter, _volumeValue);
    }
}
