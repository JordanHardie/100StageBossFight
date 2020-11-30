using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Point_Manager : MonoBehaviour
{
    public int val;
    public float speed;

    void Update()
    {
        transform.Translate(-Vector3.up * speed * Time.deltaTime);

        if(transform.position.y <= -5)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameEvents.ReportScoreChange(val);
            Destroy(gameObject);
        }
    }

    // Destroy this game object on game over
    void DestroyThis(GameState gameState)
    {
        if (gameState == GameState.GAMEOVER)
        {
            Destroy(gameObject);
        }
    }

    #region Event listening
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
