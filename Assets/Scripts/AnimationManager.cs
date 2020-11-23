using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationManager : MonoBehaviour
{
    #region Variables
    public Animator transition;
    public float transtionTime;
    public float speed;
    public bool IsBackground = false;
    public bool IsGameBG;
    #endregion

    void Update()
    {
        if(IsBackground)
            MoveBG();

        if(IsGameBG)
        {
            transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
        }
    }

    //Move background for main menu
    void MoveBG()
    {
        float MouseX = Input.GetAxis("Mouse X");
        float MouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(-MouseY, MouseX, 0);
        //Counter z rotation because for some reason when you move 
        //something on the x and y axes only the z axis changes too
        //which is really helpfull and definitely not at all frustrating for no reason hahaha.
        float z = transform.eulerAngles.z;
        transform.Rotate(0, 0, -z);
    }

    public void OnActivate()
    {
        OnGameStateChange(GameState.INGAME);
        GameEvents.ReportGameStateChange(GameState.INGAME);
    }

    void OnGameStateChange(GameState gameState)
    {
        StartCoroutine(LoadLevel(gameState));
    }

    IEnumerator LoadLevel(GameState gameState)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(transtionTime);

        if (gameState != GameState.GAMEOVER)
        {
            SceneManager.LoadScene("100SBF_" + gameState.ToString());
        }      

        else
        {
            //Do stuff
        }
    }

    #region Event listening
    void OnEnable()
    {
        GameEvents.OnGameStateChange += OnGameStateChange;
    }

    void OnDisable()
    {
        GameEvents.OnGameStateChange -= OnGameStateChange;
    }
    #endregion
}
