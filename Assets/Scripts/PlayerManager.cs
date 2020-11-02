using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public float speed;
    public Sprite bullet;

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

        //Check if user is holding shift
        switch (Input.GetKey(KeyCode.LeftShift) | Input.GetKey(KeyCode.RightShift))
        {
            //If they are, reduce speed
            case true:
                float tSpeed = speed / 2;
                transform.Translate(Vector3.right * tSpeed * Time.deltaTime * h);
                transform.Translate(Vector3.up * tSpeed * Time.deltaTime * v);
                break;

            //If they are not, move normally
            default:
                transform.Translate(Vector3.right * speed * Time.deltaTime * h);
                transform.Translate(Vector3.up * speed * Time.deltaTime * v);
                break;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        Debug.Log(collision.tag);
        if (collision.CompareTag("Bullet"))
        {
            GameEvents.ReportGrazeChange(true);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            GameEvents.ReportGrazeChange(false);
        }
    }
}
