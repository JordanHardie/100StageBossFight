using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Level_Manager : Singleton<Level_Manager>
{
    public List<Spawn> spawns;

    [Header("Refrences")]
    public TextMeshProUGUI titleCard;
    public string timerText;
    public float timer;

    public void Start()
    {
        InvokeRepeating("TimeCheck", 0, 0.1f);
    }

    void Update()
    {
        timer += Time.deltaTime;
        System.TimeSpan t = System.TimeSpan.FromSeconds(timer);
        timerText = string.Format("{0:D2}:{1:D2}:{2:D3}", t.Minutes, t.Seconds, t.Milliseconds);
    }

    void TimeCheck()
    {
        if(spawns.Count == 0)
        {
            CancelInvoke();
            //Debug.Log("Canceled Invoke");
        }

        else
        {
            for (int i = 0; i < spawns.Count; i++)
            {
                // ¯\_(ツ)_/¯
                if (spawns[i].time <= timer)
                {
                    //Debug.Log("Spawns " + spawns[i].time);
                    Instantiate(spawns[i].Enemy);
                    spawns.Remove(spawns[i]);
                }
            }
        }  
    }

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

[System.Serializable]
public class Spawn
{
    public GameObject Enemy;
    public float time;
}
