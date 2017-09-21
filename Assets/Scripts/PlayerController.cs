using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The Player Controller : component
/// This controls the Player
/// *Movement
/// *Jump
/// *Life
/// </summary>
public class PlayerController : MonoBehaviour {
    /// <summary>
    /// Variables 
    /// 
    /// </summary>
    private bool moveLeft = false;
    private bool moveRight = false;
    private float jumpCap = 3;
    private int lane = 0;
    private bool isSliding = false;
    private bool isJumping = false;
    private float jumpTimer = 0;
    private float slideTimer = 0;
    private float moveDelay = 0;
    public float speed = 10;
    public float gravity = 10;
    public float velX = 10;
    public float velY = 0;
    float GRAVITY = 0;
    Vector3 pos;
    public bool stopGravity = false;
    public float addedMoveDelay = 0;
    public float multiplier;
    public float velocity = 1;
    public float godTimer;
    public static bool isGod = false;
    public static int score = 0;
    public static int life = 0;
    //private Vector3


    // Use this for initialization
    void Start () {
        life = 1;
       // score = 0;

    }
	
	// Update is called once per frame
	void Update ()
    {
        Run();
        //UpdateScore.score++; 
        score += 5;
    }
    /// <summary>
    /// Run
    /// Player movement and axis movement
    /// </summary>
    private void Run()
    {
        //timer = 60 * Time.deltaTime;
        //print(timer);

        float jumping = Input.GetAxis("Jump");
        float movingHorizontal = Input.GetAxisRaw("Horizontal");
        float sliding = Input.GetAxis("Vertical");

        Vector3 pos = transform.position;
        pos.x += speed * Time.deltaTime;
        transform.position = pos;

        if (stopGravity == false)
        {
            float accY = gravity * Time.deltaTime;
            GRAVITY = (gravity * Time.deltaTime) * 2;
            pos.y += velY - GRAVITY;
            transform.position = pos;
        }


        if (jumping > 0 && jumpTimer < .4f)
        {
            //velocity = gravity * Time.deltaTime;
            jumpTimer += speed * Time.deltaTime;
            Jump();
        }
        else
        {
            velY = 0;
        }

        if (movingHorizontal > 0 && moveLeft == false && moveRight == false && moveDelay <= 0)
        {
            moveRight = true;
        }
        else if (movingHorizontal < 0 && moveRight == false && moveLeft == false && moveDelay <= 0)
        {
            moveLeft = true;
        }

        if (moveRight == true || moveLeft == true)
        {
            SwapLanes();
            moveDelay = .5f + addedMoveDelay;
        }

        if (sliding < 0)
        {
            isSliding = true;
            slideTimer += .2f;
            Slide();
        }
        else
        {
            isSliding = false;
            GetComponent<AABB>().halfSize.y = .5f;
            speed = 5;
            slideTimer = 0;

        }

        if (transform.localPosition.y >= jumpCap)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, jumpCap);
        }

        if (isSliding == false)
        {
            transform.localScale = Vector3.one;
        }

        if (moveDelay > 0)
        {
            moveDelay = moveDelay - Time.deltaTime;
        }



        if (lane > 1)
        {
            lane--;
        }
        if (lane < -1)
        {
            lane++;
        }
        moveLane();
    }

    /// <summary>
    /// 
    /// </summary>
    void Jump()
    {
        //print("Jumping");
        //transform.Translate(new Vector3(0, (jumpTimer * 10), 0));
        velY += speed * Time.deltaTime;
        pos.y = velY;

    }
    /// <summary>
    /// 
    /// </summary>
    void Slide()
    {
        //print("Sliding");
        
        transform.localScale = new Vector3(transform.localScale.x, .5f, transform.localScale.z);
        if(speed > 3)
        {
            speed = 10 - slideTimer * 3 * Time.deltaTime;
        }
        
        GetComponent<AABB>().halfSize.y = .25f;

    }
    /// <summary>
    /// 
    /// </summary>
    void SwapLanes()
    {

        if(moveRight == true)
        {
            lane--;
            moveRight = false;
        }
        if(moveLeft == true)
        {
            lane++;
            moveLeft = false;
        }

        //print(moveAmount);
    }

    /// <summary>
    /// 
    /// </summary>
    void moveLane()
    {
        if(lane == -1)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -3);
        } else if(lane == 0)
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 0);
        } else
        {
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, 3);
        }
    }
    /// <summary>
    /// /Apply a collision "fix" to prevent collision detection
    /// Apply Fix 
    /// Fixes the player position and moves them to a new position
    /// </summary>
    /// <paramHow far to move the player

    public void ApplyFix(Vector3 fix)
    {

        transform.position += fix;
        GetComponent<AABB>().calcEdges();

        if(fix.x != 0)
        {

            //zero x velocity
        }
        if (fix.y != 0)
        {
            //zero y velocity
            if (isJumping != true)
            {
                GRAVITY = 0;
                jumpTimer = 0;
            }
        }
        if (fix.z != 0)
        {
            //zero x velocity
        }

    }

}
