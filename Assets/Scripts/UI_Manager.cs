using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UI_Manager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI multiText;
    int score;
    double multi = 1.0;

    void Update()
    {
        scoreText.text = score.ToString();
        multiText.text = multi + "x";
    }

    void Score(int val)
    {
        double math = val * multi;
        math = Math.Round(math);
        int result = (int)math;
        score += result;
    }

    void OnGraze(bool isGrazing)
    {
        if (isGrazing)
        {
            multi += 0.1f;
            multi = Math.Round(multi, 3);
        }
    }

    void OnEnable()
    {
        GameEvents.OnGraze += OnGraze;
        GameEvents.OnScoreChange += Score;
    }

    private void OnDisable()
    {
        GameEvents.OnGraze -= OnGraze;
        GameEvents.OnScoreChange -= Score;
    }
}
