using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [Header("Audio")]
    public AudioSource[] audioSources;
    public Slider audioSlider;
    public Toggle muteToggle;

    void Start()
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].volume = audioSlider.value = 1;
        }
    }

    public void ChangeVolume(float _value)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].volume = _value;
        }
    }

    public void ToggleMute(bool _value)
    {
        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].mute = _value;
        }
    }
}
