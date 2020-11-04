using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BulletType
{
    STRAIGHT,
    HOMING,
    ZIGZAG,
    SPIN
}

public class BulletManager : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float lifeTime;
    public BulletType bulletType;
    bool temp = true;

    void Update()
    {
        lifeTime -= Time.deltaTime;

        if (lifeTime <= 0)
        {
            Destroy(gameObject);
        }

        else
        {
            switch (bulletType)
            {
                case BulletType.STRAIGHT:
                    if(temp)
                    {
                        LookAtPlayer();
                        temp = false;
                    }

                    transform.Translate(Vector3.up * speed * Time.deltaTime);
                    break;

                case BulletType.HOMING:
                    LookAtPlayer();
                    transform.Translate(Vector3.up * speed * Time.deltaTime);
                    break;
            }
        }
    }

    void LookAtPlayer()
    {
        float angle = 0;
        Vector3 relative = transform.InverseTransformPoint(player.transform.position);
        angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
        transform.Rotate(0, 0, -angle);
    }
}
