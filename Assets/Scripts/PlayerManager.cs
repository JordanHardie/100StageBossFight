using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float speed;
    public GameObject bullet;

    #region Boundary stuff
    public Camera MainCamera;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        screenBounds = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, MainCamera.transform.position.z));
        objectWidth = transform.GetComponent<SpriteRenderer>().bounds.extents.x; //extents = size of width / 2
        objectHeight = transform.GetComponent<SpriteRenderer>().bounds.extents.y; //extents = size of height / 2
    }

    void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, screenBounds.x * -1 + objectWidth, screenBounds.x - objectWidth);
        viewPos.y = Mathf.Clamp(viewPos.y, screenBounds.y * -1 + objectHeight, screenBounds.y - objectHeight);
        transform.position = viewPos;
    }
    #endregion

    void FixedUpdate()
    {
        Moving();
    }

    //Chuck it all into a function to make it easier to read I suppose
    void Moving()
    {
        //Get dem axes
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.D))
        {
            if(IsInvoking())
            {
                CancelInvoke();
            }
            
            else
            {
                InvokeRepeating("Shoot", 0f, 1000f);
            }
        }

        else
        {
            CancelInvoke();
        }

        switch (Input.GetKey(KeyCode.LeftShift) | Input.GetKey(KeyCode.RightShift))
        {
            case true:
                float tSpeed = speed / 2;
                transform.Translate(Vector3.right * h * tSpeed * Time.deltaTime);
                transform.Translate(Vector3.up * v * tSpeed * Time.deltaTime);
                break;

            default:
                transform.Translate(Vector3.right * h * speed * Time.deltaTime);
                transform.Translate(Vector3.up * v * speed * Time.deltaTime);
                break;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            GameEvents.ReportHit(true);
            Destroy(collision.gameObject);
        }
    }

    void Shoot()
    {
        Instantiate(bullet, transform.position, transform.rotation);
    }
}
