using UnityEngine;
using UnityEngine.AI;

public class EnemyManager : MonoBehaviour
{
    #region Variables
    [Header("References")]
    public GameObject[] barrels;
    public GameObject[] points;
    public GameObject player;
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

    GameObject target;
    float fix;
    float rotFix;
    float rateFix;
    #endregion

    void Start()
    {
        // Assign a random point to move towards
        int i = Random.Range(0, points.Length);

        // For reseting timers
        fix = interval;
        rotFix = rotInterval;
        rateFix = rate;
        target = points[i];
    }

    void Update()
    {
        DoStuff();
    }

    void DoStuff()
    {
        if (health <= 0)
        {
            GameEvents.ReportScoreChange(defeatScore);
            Destroy(gameObject);
        }

        Move(target);
        LookAtPlayer();
        Fire();
    }

    void Fire()
    {
        TimerFunction(rate, 0);
        TimerFunction(rotInterval, 1);
        TimerFunction(interval, 2);
    }

    void Move(GameObject _target)
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, _target.transform.position, step);

        if (Vector3.Distance(transform.position, _target.transform.position) < 0.001f)
        {
            int i = Random.Range(0, points.Length);
            target = points[i];
        }
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
            GameEvents.ReportScoreChange(hitScore);
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