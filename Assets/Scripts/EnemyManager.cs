using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public float speed;
    public float interval;
    public int health;
    float fix;

    [SerializeField]
    GameObject player;
    [SerializeField]
    GameObject bullet;
    [SerializeField]
    GameObject barrel;

    void Start()
    {
        fix = interval;
    }

    // Update is called once per frame
    void Update()
    {
        LookAtPlayer();
        Fire();
    }

    void Fire()
    {
        interval -= Time.deltaTime;

        if (interval <= 0 && health > 0)
        {
            Instantiate(bullet, barrel.transform.position, barrel.transform.rotation);
            interval = fix;
        }

        else if (health <= 0)
        {
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
}
