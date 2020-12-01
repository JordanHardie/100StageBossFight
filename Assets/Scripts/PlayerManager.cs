using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region Variables
    public GameObject bullet;
    public GameObject[] barrels;
    public Animator animator;
    public float angle;
    public float timer;
    public float speed;
    float fix;
    #endregion

    #region Boundary stuff
    public Camera MainCamera;
    private Vector2 screenBounds;
    private float objectWidth;
    private float objectHeight;

    void Start()
    {
        fix = timer;

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

    // Movement in fixed update so collisions are properly dectected
    void FixedUpdate()
    {
        Moving();
    }

    // Check when we hit a bullet
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            GameEvents.ReportHit(true);
            Destroy(collision.gameObject);
        }
    }

    //Chuck it all into a function to make it easier to read I suppose
    void Moving()
    {
        //Get dem axes
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.W) | Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.S) | Input.GetKey(KeyCode.D))
        {
            if (Input.GetKey(KeyCode.A))
            {
                animator.SetTrigger("GoingLeft");
            }

            else if (Input.GetKey(KeyCode.D))
            {
                animator.SetTrigger("GoingRight");
            }

            else
            {
                animator.SetTrigger("Neither");
            }

            Shoot();
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

    void Shoot()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            for (int i = 0; i < barrels.Length; i++)
            {
                GameObject barrel = barrels[i];
                Instantiate(bullet, barrel.transform.position, barrel.transform.rotation);
            }
            timer = fix;
        }
    }

    void DestroyThis(GameState gameState)
    {
        if (gameState == GameState.GAMEOVER)
        {
            Destroy(gameObject);
        }
    }

    #region Event listening
    void OnEnable()
    {
        GameEvents.OnGameStateChange += DestroyThis;
    }

    void OnDisable()
    {
        GameEvents.OnGameStateChange -= DestroyThis;
    }
    #endregion
}
