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
    SpriteRenderer spriteRenderer;
    public GameObject player;
    public float speed;
    public float lifeTime;
    public BulletType bulletType;
    bool temp = true;
    bool temp2 = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if(spriteRenderer.isVisible)
        {
            //Yay
        }

        else
        {
            //Destroy(gameObject);
        }

        switch (lifeTime <= 0)
        {
            case true:
                Destroy(gameObject);
                break;

            case false:

                lifeTime -= Time.deltaTime;

                switch(CompareTag("PlayerBullet"))
                {
                    case true:
                        transform.Translate(Vector3.up * speed * Time.deltaTime);
                        break;

                    case false:
                        switch (bulletType)
                        {
                            case BulletType.STRAIGHT:
                                if (temp)
                                {
                                    LookAtPlayer();
                                    temp = false;
                                }

                                transform.Translate(Vector3.up * speed * Time.deltaTime);
                                break;

                            case BulletType.HOMING:
                                if(transform.position.y <= player.transform.position.y)
                                {
                                    transform.Translate(Vector3.up * speed * Time.deltaTime);
                                    temp2 = true;
                                }

                                else if(temp2)
                                {
                                    transform.Translate(Vector3.up * speed * Time.deltaTime);
                                }

                                else
                                {
                                    LookAtPlayer();
                                    transform.Translate(Vector3.up * speed * Time.deltaTime);
                                }
                                break;
                        }
                        break;

                }
                break;
        }
        
    }

    void LookAtPlayer()
    {
        Vector3 relative = transform.InverseTransformPoint(player.transform.position);
        float angle = Mathf.Atan2(relative.x, relative.y) * Mathf.Rad2Deg;
        transform.Rotate(0, 0, -angle);
    }
}
