using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    #region Variables
    public GameObject[] barrels;
    public GameObject player;
    public GameObject bullet;

    public float interval;
    public int health;
    public int score;

    [Header("Spinning barrel")]
    public float angle;
    public float rate;
    public float rotInterval;

    float fix;
    float rotFix;
    float rateFix;
    #endregion

    void Start()
    {
        fix = interval;
        rotFix = rotInterval;
        rateFix = rate;
    }

    void Update()
    {
        DoStuff();
    }

    void DoStuff()
    {
        if (health <= 0)
        {
            GameEvents.ReportScoreChange(score);
            Destroy(gameObject);
        }

        LookAtPlayer();
        Fire();
    }

    void Fire()
    {
        TimerFunction(rate, 0);
        TimerFunction(rotInterval, 1);
        TimerFunction(interval, 2);
    }

    void LookAtPlayer()
    {
        Vector3 relative = transform.InverseTransformPoint(player.transform.position);
        float angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
        transform.Rotate(0, 0, -angle);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            health -= 1;
            GameEvents.ReportScoreChange(1000);
            Destroy(collision.gameObject);
        }
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
                    barrel.transform.Rotate(0, 0, angle);

                if (val == 2)
                        Instantiate(bullet, barrel.transform.position, barrel.transform.rotation);
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

    void OnEnable()
    {
        GameEvents.OnGameStateChange += DestroyThis;
    }

    void OnDisable()
    {
        GameEvents.OnGameStateChange -= DestroyThis;
    }
}