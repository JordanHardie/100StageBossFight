using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UI_Manager : MonoBehaviour
{
    public int lives;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI multiText;
    public GameObject panel;
    public TextMeshProUGUI[] scores;
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
            multi += 0.05f;
            multi = Math.Round(multi, 4);
        }
    }

    void GameOver(bool isHit)
    {
        if(isHit && lives < 1)
        {
            Time.timeScale = 0;

            for (int i = 0; i < scores.Length; i++)
            {
                TextMeshProUGUI score = scores[i];
                score.text = "";
            }

            panel.SetActive(true);
           

            for (int i = 0; i < scores.Length; i++)
            {
                
                switch(i)
                {
                    case 0:
                        scores[i].text = score.ToString();
                        break;
                }
            }
        }
    }

    IEnumerator scoreReveal()
    {
        yield return new WaitForSecondsRealtime(5f);
    }

    void OnEnable()
    {
        GameEvents.OnHit += GameOver;
        GameEvents.OnGraze += OnGraze;
        GameEvents.OnScoreChange += Score;
    }

    private void OnDisable()
    {
        GameEvents.OnGraze -= OnGraze;
        GameEvents.OnScoreChange -= Score;
    }
}
