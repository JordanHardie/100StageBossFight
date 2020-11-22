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
    string rank = "S++";
    float destruction = 100f;
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
            for (int i = 0; i < scores.Length; i++)
            {
                TextMeshProUGUI score = scores[i];
                score.text = "";
            }

            panel.SetActive(true);

            StartCoroutine(ScoreReveal());
        }
    }

    IEnumerator ScoreReveal()
    {
        for (int i = 0; i < scores.Length; i++)
        {
            yield return new WaitForSecondsRealtime(1f);
            TextMeshProUGUI _score = scores[i];
            switch (i)
            {
                #region Case 0
                case 0:
                    _score.text = score.ToString("0#,###0");
                    break;
                #endregion

                #region Case 1
                case 1:
                    _score.text = multi + "x";
                    break;
                #endregion

                #region Case 2
                case 2:
                    Difficulty difficulty = GameManager.Instance.difficulty;

                    switch(difficulty)
                    {
                        case Difficulty.EASY:
                            _score.text = "EASY [1.5x]";
                            break;

                        case Difficulty.MEDIUM:
                            _score.text = "MEDIUM [3x]";
                            break;

                        case Difficulty.HARD:
                            _score.text = "HARD [5x]";
                            break;

                        case Difficulty.INSANE:
                            _score.text = "INSANE [8x]";
                            break;

                        case Difficulty.HEAVEN:
                            _score.text = "HEAVEN [10x]";
                            break;
                    }

                    break;
                #endregion

                #region Case 3
                case 3:
                    _score.text = destruction + "%";
                    break;
                #endregion

                #region Case 4
                case 4:
                    _score.text = rank;
                    break;
                #endregion

                #region Case 5
                case 5:
                    yield return new WaitForSecondsRealtime(0.5f);
                    float step = (destruction * 2) / 100;
                    double step1 = score * multi * step; //* Ranking multi
                    string result = step1.ToString("0#,###0");

                    _score.text = result;
                    break;
                #endregion
            }
        }
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
