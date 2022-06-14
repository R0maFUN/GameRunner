using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    private Rigidbody rb;
    private GameObject checkGroundObject;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] public IPower superPower;

    //private Score score;

    private float horizontalInput = 0f;
    private float verticalInput = 0f;
    private float yTouchStart = 0f;
    private int currentScore = 0;

    [SerializeField] public float horizontalForce = 1000f;
    [SerializeField] public float jumpPower = 200f;
    [SerializeField] private float bonusGravity = 9.8f;

    private bool jumpRequested = false;
    private bool downRequested = false;
    private bool isInTheAir = false;
    private bool isUsingSuperPower = false;

    // Start is called before the first frame update
    void Start()
    {
        checkGroundObject = GameObject.Find("GroundChecker");
        superPower = GetComponent<IPower>();
        //score = FindObjectOfType<Score>();
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Check is Player staying on smth
        if (Physics.OverlapSphere(checkGroundObject.transform.position, 0.05f, playerMask).Length >= 1)
            isInTheAir = false;
        else
            isInTheAir = true;

        // Check for horizontal input
        if (horizontalInput != 0)
            horizontalMove();

        // Check for jump requested
        if (jumpRequested && !isInTheAir)
            jump();

        // Check for get down requested
        if (downRequested && isInTheAir)
        {
            rb.AddForce(0, -jumpPower, 0, ForceMode.Impulse);
            downRequested = false;
        }

        // Additional gravity to get down faster
        if (isInTheAir)
        {
            Vector3 velocity = rb.velocity;
            velocity.y -= bonusGravity * Time.deltaTime;
            rb.velocity = velocity;
        }

    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        //verticalInput = Input.GetAxisRaw("Vertical");

        foreach (Touch touch in Input.touches)
        {
            //Debug.Log("touch pos: " + touch.position.x + " " + touch.position.y);
            if (touch.phase == TouchPhase.Began)
            {
                if (touch.position.x <= Screen.width / 2 - Screen.width * 0.2)
                {
                    horizontalInput = -1;
                }
                else if (touch.position.x >= Screen.width / 2 + Screen.width * 0.2)
                {
                    horizontalInput = 1;
                }
                yTouchStart = touch.position.y;
            }
            if (touch.phase == TouchPhase.Ended)
            {
                horizontalInput = 0;
                if (touch.position.y >= yTouchStart + 50 && !isInTheAir)
                    jumpRequested = true;
                else if (touch.position.y <= yTouchStart - 50 && isInTheAir)
                    downRequested = true;
            }
        }

        if (Input.GetKeyDown("w") && !isInTheAir)
            jumpRequested = true;
        if (Input.GetKeyDown("s") && isInTheAir)
            downRequested = true;

        if (Input.GetKeyDown("e"))
        {
            if (isUsingSuperPower)
            {
                superPower.StopPower();
                isUsingSuperPower = false;
            }
            else
            {
                superPower.StartPower();
                isUsingSuperPower = true;
            }
        }

        if (Input.GetKeyDown("i"))
        {
            Debug.Log("Going to inventory");
            //SceneManager.LoadScene("Inventory");
        }
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "SpawnTrigger")
    //    {
    //        if (GameManager.instance.currentScore > 4)
    //            RoadGenerator.instance.RespawnFirstRoad();

    //        GameManager.instance.currentScore++;
    //        makeItHarder();
    //        //score.UpdateScore(currentScore);
    //    }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Obstacle")
        {
            Debug.Log("Game Over");
            GameManager.instance.GameOver();
        }
    }

    private void makeItHarder()
    {
        RoadGenerator.instance.increaseSpeed();
    }

    private void horizontalMove()
    {
        if (horizontalInput < 0 && transform.position.x - transform.lossyScale.x / 2 - 0.2 <= RoadGenerator.instance.minX ||
            horizontalInput > 0 && transform.position.x + transform.lossyScale.x / 2  >= RoadGenerator.instance.maxX)
            return;
        Vector3 direction = new Vector3(horizontalInput, 0, 0);
        transform.Translate(direction * horizontalForce * Time.deltaTime);

    }
    private void jump()
    {
        rb.AddForce(0, jumpPower, 0, ForceMode.Impulse);
        jumpRequested = false;
        isInTheAir = true;
    }
}
