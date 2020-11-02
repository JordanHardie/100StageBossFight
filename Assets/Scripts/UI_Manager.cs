using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    int score;
    float multi;

    void Update()
    {

    }

    void Score(int val)
    {
        float math = val * multi;
        math = Mathf.RoundToInt(math);
        int result = (int)math;
        score += result;
    }

    void OnGraze(bool isGrazing)
    {
        if (isGrazing)
        {
            multi += 2f;
            scoreText.text = multi.ToString();
        }
    }

    void OnEnable()
    {
        GameEvents.OnGraze += OnGraze;
    }

    private void OnDisable()
    {
        GameEvents.OnGraze -= OnGraze;
    }
}
