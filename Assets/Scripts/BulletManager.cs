using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    [SerializeField]
    enum BulletType
    {
        STRAIGHT,
        HOMING,
        ZIGZAG,
        SPIN
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
