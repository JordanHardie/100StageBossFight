using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
{
    #region Variables
    // Public
    public GameObject bullet;
    public GameObject[] barrels;
    public Animator animator;
    public float angle;
    public float timer;
    public float speed;

    // Private
    float fix;
    bool flip = true;
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

    // Chuck it all into a function to make it easier to read I suppose
    void Moving()
    {
        //Get dem axes
        float v = Input.GetAxis("Vertical");
        float h = Input.GetAxis("Horizontal");

        if (Input.GetAxis("Horizontal") != 0 | Input.GetAxis("Vertical") != 0)
        {
            
            if (Input.GetKey(KeyCode.A) | Input.GetKey(KeyCode.LeftArrow))
            {
                animator.SetTrigger("GoingLeft");
            }

            else if (Input.GetKey(KeyCode.D) | Input.GetKey(KeyCode.RightArrow))
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
                timer -= Time.deltaTime;
                transform.Translate(Vector3.right * h * tSpeed * Time.deltaTime);
                transform.Translate(Vector3.up * v * tSpeed * Time.deltaTime);
                break;

            default:
                transform.Translate(Vector3.right * h * speed * Time.deltaTime);
                transform.Translate(Vector3.up * v * speed * Time.deltaTime);
                break;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            AudioManager.Instance.PlaySound(2);
            flip = !flip;
            UI_Manager.Instance.Pause(flip);
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
            // Game manager reduces lives and all enemy bullets are converted into points
            GameEvents.ReportHit(true);
            Destroy(collision.gameObject);
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
