﻿using System.Collections;
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

    [Header("Zig zag Options")]
    public float timer;
    public float angle;
    float setTimer;

    [Header("Bullet type")]
    public BulletType bulletType;
    bool temp = true;
    bool temp2 = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        setTimer = timer;
        if(bulletType == BulletType.ZIGZAG)
        {
            transform.Rotate(0, 0, angle);
        }
    }

    void Update()
    {
        switch (lifeTime <= 0)
        {
            //Destroy the game object after it's lifespan
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
                            #region Straight Bullet
                            case BulletType.STRAIGHT:
                                transform.Translate(Vector3.up * speed * Time.deltaTime);
                                break;
                            #endregion

                            #region Homing Bullet
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
                            #endregion

                            #region Zig Zag Bullet
                            case BulletType.ZIGZAG:
                                timer -= Time.deltaTime;
                                transform.Translate(Vector3.up * speed * Time.deltaTime);

                                if (timer <= 0)
                                {
                                    temp = !temp;
                                    if (temp)
                                    {
                                        transform.Rotate(0, 0, angle * 2);
                                    }

                                    else
                                    {
                                        transform.Rotate(0, 0, -angle * 2);   
                                    }

                                    timer = setTimer;
                                    
                                }
                                break;
                                #endregion
                        }
                        break;
                }
                break;
        }   
    }

    void DestroyThis(GameState gameState)
    {
        if(gameState == GameState.GAMEOVER)
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

    void OnEnable()
    {
        GameEvents.OnGameStateChange += DestroyThis;
    }

    void OnDisable()
    {
        GameEvents.OnGameStateChange -= DestroyThis;
    }
}
