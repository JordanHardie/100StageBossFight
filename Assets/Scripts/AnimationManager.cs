using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationManager : MonoBehaviour
{
    public Animator transition;
    public float transtionTime;
    public bool IsBackground = false;

    void Update()
    {
        if(IsBackground == true)
        {
            MoveBG();
        }
    }

    void MoveBG()
    {
        float MouseX = Input.GetAxis("Mouse X");
        float MouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(-MouseY, MouseX, 0);
        float z = transform.eulerAngles.z;
        transform.Rotate(0, 0, -z);
    }

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
