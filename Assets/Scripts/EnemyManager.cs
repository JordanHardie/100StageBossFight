using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public GameObject[] points;
    public GameObject[] barrels;
    public float interval;
    public int health;
    public int score;
    float fix;

    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject bullet;


    void Start()
    {
        fix = interval;
    }

    // Update is called once per frame
    void Update()
    {
        DoStuff();
    }

    void Fire()
    {
        interval -= Time.deltaTime;
        if (interval <= 0 && health > 0)
        {
            for (int i = 0; i < barrels.Length; i++)
            {
                GameObject barrel = barrels[i];
                Instantiate(bullet, barrel.transform.position, barrel.transform.rotation);  
            }
            interval = fix;
        }

        else if (health <= 0)
        {
            GameEvents.ReportScoreChange(score);
            Destroy(gameObject);
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
        if(collision.CompareTag("PlayerBullet"))
        {
            health -= 1;
            GameEvents.ReportScoreChange(1000);
            Destroy(collision.gameObject);
        }
    }

    void DoStuff()
    {
        LookAtPlayer();
        Fire();
    }
}
