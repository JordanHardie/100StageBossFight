using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public GameObject player;
    public float speed;
    public float lifeTime;

    public enum BulletType
    {
        STRAIGHT,
        HOMING,
        ZIGZAG,
        SPIN
    }

    BulletType bulletType;

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
                    //transform.LookAt(player.transform.position);
                    transform.Translate(-Vector3.up * speed * Time.deltaTime);
                    break;

                default:
                    break;
            }
        }
    }

    void OnCollisionEnter(Collision other)
    {
        Debug.Log(other);
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag);
    }
}
