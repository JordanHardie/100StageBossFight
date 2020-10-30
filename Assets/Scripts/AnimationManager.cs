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
    #endregion

    void Update()
    {
        if(IsBackground)
            MoveBG();
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

    public void OnClick()
    {
        GameEvents.ReportGameStateChange(GameState.MENU);
    }

    void OnGameStateChange(GameState gameState)
    {
        if (gameState != GameState.TITLE)
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
