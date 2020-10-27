using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float speed;
    public Sprite bullet;

    public Camera MainCamera;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    // Use this for initialization
    void Start()
    {
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
        transform.position = viewPos;
    }

    void FixedUpdate()
    {
        Moving();
    }

    void Moving()
    {
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");


        switch (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.D))
        {
            case true:
                ///Vector3 pos = new Vector3(transform.position.x, transform.position.y, transform.position.z);
                ///Instantiate(bullet, pos, transform.rotation);

                switch (Input.GetKey(KeyCode.LeftShift) | Input.GetKey(KeyCode.RightShift))
                {
                    case true:
                        float tSpeed = speed / 2;
                        transform.Translate(Vector3.right * tSpeed * Time.deltaTime * h);
                        transform.Translate(Vector3.up * tSpeed * Time.deltaTime * v);
                        break;

                    case false:
                        transform.Translate(Vector3.right * speed * Time.deltaTime * h);
                        transform.Translate(Vector3.up * speed * Time.deltaTime * v);
                        break;
                }
                break;

            case false:
                break;
        }
    }
}
