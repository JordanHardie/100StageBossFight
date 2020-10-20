﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationManager : MonoBehaviour
{
    public Animator transition;
    public float transtionTime;

    public void OnClick()
    {
        GameEvents.ReportGameStateChange(GameState.MENU);
    }

    void OnGameStateChange(GameState gameState)
    {
        StartCoroutine(LoadLevel(gameState));
    }

    IEnumerator LoadLevel(GameState gameState)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transtionTime);

        SceneManager.LoadScene("100SBF_" + gameState.ToString());
    }

    void OnEnable()
    {
        GameEvents.OnGameStateChange += OnGameStateChange;
    }

    void OnDisable()
    {
        GameEvents.OnGameStateChange -= OnGameStateChange;
    }
}
