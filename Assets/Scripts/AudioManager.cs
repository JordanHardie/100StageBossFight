using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : Singleton<AudioManager>
{
    [Header("Audio")]
    public AudioSource[] audioSources;
    public AudioClip[] audioClips;
    public Slider audioSlider;
    public Toggle muteToggle;
    AudioSource normal;
    AudioSource ambient;

    bool flip = true;
    bool alsoFlip;

    void Start()
    {
        normal = audioSources[1];
        ambient = audioSources[2];

        for (int i = 0; i < audioSources.Length; i++)
        {
            audioSources[i].volume = audioSlider.value = 0.7f;

            if(i == 2)
            {
                audioSources[i].volume = 0.1f;
            }
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
        if (GameManager.Instance.gameState == GameState.TITLE)
        {
            for(int i = 0; i < audioSources.Length; i++)
            {
                audioSources[i].mute = _value;
            }
        }

        else
        {
            for(int i = 1; i < audioSources.Length; i++)
            {
                audioSources[i].mute = _value;

                if (i == 2)
                {
                    audioSources[i].volume = 0.1f;
                }
            } 
            PreventMuteChange(_value);
        } 
    }

    void PreventMuteChange(bool _value)
    {
        alsoFlip = _value;
    }

    public void PlaySound(int index)
    {
        switch(index)
        {
            #region Case zero
            case (0):
                normal.PlayOneShot(audioClips[Random.Range(0, 2)]);
                break;
            #endregion

            #region Case one
            case (1):
                normal.PlayOneShot(audioClips[Random.Range(2, 4)]);
                break;
            #endregion

            #region Case two
            case (2):
                if(flip)
                {
                    audioSources[0].mute = true;
                    normal.PlayOneShot(audioClips[Random.Range(4, 6)]);
                    flip = !flip;
                }

                else
                {
                    if (alsoFlip)
                    {
                        flip = !flip;
                    }

                    else
                    {
                        audioSources[0].mute = false;
                        flip = !flip;
                    }     
                }
                break;
            #endregion

            #region Case three
            case (3):
                ambient.PlayOneShot(audioClips[Random.Range(6, 8)]);
                break;
           #endregion
        }
    }

    void DestroyThis(GameState gameState)
    {
        if (gameState == GameState.INGAME)
        {
            Destroy(gameObject);
        }
    }

    #region Event Listening
    void OnEnable()
    {
        GameEvents.OnGameStateChange += DestroyThis;
    }

    void OnDisable()
    {
        GameEvents.OnGameStateChange -= DestroyThis;
    }
    #endregion
}
