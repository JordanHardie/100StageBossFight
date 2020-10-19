using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationManager : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.Rotate(transform.forward * speed * Time.deltaTime);
    }

    public void Initiate()
    {

    }
}
