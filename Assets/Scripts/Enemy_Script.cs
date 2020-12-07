using UnityEngine;
using System.Collections.Generic;

public class Enemy_Script : MonoBehaviour
{
    #region Variables
    [Header("References")]
    // Public Variables
    public GameObject[] barrels;
    public GameObject bullet;

    [Header("Ship values")]
    public float speed;
    public float interval;
    public int health;
    public int hitScore;
    public int defeatScore;

    [Header("Spinning barrel")]
    public float angle;
    public float rate;
    public float rotInterval;

    // Private Variables
    GameObject player;
    public List<Vector3> points;
    Vector3 target;
    float fix;
    float rotFix;
    float rateFix;
    #endregion

    void Start()
    {
        player = PlayerManager.Instance.gameObject;
        // Creates a list of movement points
        float start_x = -7;
        float start_y = 4;

        for(int i = 0; i < 5; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                points.Add(new Vector3(start_x, start_y, 0));
                start_y -= 2;
            }
            start_x += 3.5f;
            start_y = 4;
        }

        // Assign a random point to move towards
        int x = Random.Range(0, points.Count);

        // For reseting timers
        fix = interval;
        rotFix = rotInterval;
        rateFix = rate;
        target = points[x];
    }

    void Update()
    {
        DoStuff();
    }

    void DoStuff()
    {
        //Move(target);
        LookAtPlayer();
        Fire();
    }

    void Fire()
    {
        TimerFunction(rate, 0);
        TimerFunction(rotInterval, 1);
        TimerFunction(interval, 2);
    }

    void Move(Vector3 _target)
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _target, step);

        if (Vector3.Distance(transform.position, _target) < 0.001f)
        {
            int i = Random.Range(0, points.Count);
            target = points[i];
        }
    }

    void LookAtPlayer()
    {
        Vector3 relative = transform.InverseTransformPoint(player.transform.position);
        float angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
        transform.Rotate(0, 0, -angle);
    }

    void TimerFunction(float timer, int val)
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            for (int i = 0; i < barrels.Length; i++)
            {
                GameObject barrel = barrels[i];

                if (val == 0 && barrel.CompareTag("Spin"))
                    Instantiate(bullet, barrel.transform.position, barrel.transform.rotation);

                if (val == 1 && barrel.CompareTag("Spin"))
                {
                    barrel.transform.Rotate(0, 0, angle);
                }
                        

                if (val == 2)
                {
                    Instantiate(bullet, barrel.transform.position, barrel.transform.rotation);
                }
            }

            switch (val)
            {
                case 0:
                    rate = rateFix;
                    break;

                case 1:
                    rotInterval = rotFix;
                    break;

                case 2:
                    interval = fix;
                    break;
            }
        }

        else
        {
            switch (val)
            {
                case 0:
                    rate = timer;
                    break;

                case 1:
                    rotInterval = timer;
                    break;

                case 2:
                    interval = timer;
                    break;
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (health <= 0)
        {
            GameEvents.ReportScoreChange(defeatScore);
            Destroy(gameObject);
        }

        // If the enemy collides with the players bullet
        else if (collision.CompareTag("PlayerBullet"))
        {
            health -= 1;
            GameEvents.ReportScoreChange(hitScore);
            Destroy(collision.gameObject);
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