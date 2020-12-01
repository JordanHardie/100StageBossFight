using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class UI_Manager : Singleton<UI_Manager>
{
    #region Variables
    [Header("Text assignment")]
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI multiText;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI[] scores;

    [Header("Options settings")]
    public GameObject panel;
    public Slider volumerSlider;
    public TMP_InputField speedVal;
    GameManager GM;

    string rank = "S++";
    float destruction = 100f;
    int score;
    double multi = 1.0;
    #endregion

    void Start()
    {
        GM = GameManager.Instance;
        livesText.text = "Lives: " + GM.lives;
    }

    void Update()
    {
        scoreText.text = score.ToString("0#,###0");
        multiText.text = multi + "x";
    }

    void Score(int val)
    {
        // Take the score and multiply it by the current multiplier
        double math = val * multi;
        // Round it to a whole number
        math = Math.Round(math);
        // Convert it to int
        int result = (int)math;
        score += result;
    }

    // Set's scores text active I.E. revealing them
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
                    //print("0 Ran");
                    break;
                #endregion

                #region Case 1
                case 1:
                    _score.text = multi + "x";
                    //print("1 Ran");
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
                    //print("2 Ran");

                    break;
                #endregion

                #region Case 3
                case 3:
                    _score.text = destruction + "%";
                    //print("3 Ran");
                    break;
                #endregion

                #region Case 4
                case 4:
                    _score.text = rank;
                    //print("4 Ran");
                    break;
                #endregion

                #region Case 5
                case 5:
                    //print("5 Ran");
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

    #region Game manager events
    public void Graze()
    {
        multi += 0.05f;
        multi = Math.Round(multi, 4);
    }

    // Update lives text
    public void Hit()
    {
        livesText.text = "Lives: " + GM.lives;
    }

    public void Death()
    {
        // Set all score values to be blank
        for (int i = 0; i < scores.Length; i++)
        {
            TextMeshProUGUI score = scores[i];
            score.text = "";
        }

        // Activate game over panel
        panel.SetActive(true);

        // Reveal scores
        StartCoroutine(ScoreReveal());
    }
    #endregion

    #region Event listening
    void OnEnable()
    {
        GameEvents.OnScoreChange += Score;
    }

    void OnDisable()
    {
        GameEvents.OnScoreChange -= Score;
    }
    #endregion
}
