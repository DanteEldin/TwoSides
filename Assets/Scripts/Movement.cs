using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    //Variables
    KeyCode leftKey = KeyCode.A;
    KeyCode rightKey = KeyCode.D;
    KeyCode upKey = KeyCode.W;
    KeyCode downKey = KeyCode.S;
    KeyCode interactKey = KeyCode.Space;

    int horizontalAxis = 0;
    int verticalAxis = 0;
    public Vector2 vel;
    public bool standardMovement = true;
    public bool canMove = true;
    public bool canFlip = true;
    bool moving = false;
    public float walkAcc = 4.5f;
    public float walkDec = 4.5f;
    public float walkMaxSpeed = 7.0f;
    float acc = 0f;
    float dec = 0f;
    float maxSpeed = 0f;
    float speed = 0f;

    public bool nextSceneOnGoalCollide = false;
    public string nextScene;
    internal bool interacting = false;

    //Components
    SpriteRenderer spriteRenderer;
    Rigidbody2D rbody;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rbody = GetComponent<Rigidbody2D>();
        
        acc = walkAcc;
        dec = walkDec;
        maxSpeed = walkMaxSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        Move();

        //Parse movement
        if (standardMovement)
        {
            rbody.velocity = new Vector2(vel.x, vel.y);
        }
    }

    void GetInput()
    {
        //Move
        moving = false;
        horizontalAxis = 0;
        verticalAxis = 0;

        if (canMove)
        {
            //Horizontal
            if (Input.GetKey(leftKey) && !Input.GetKey(rightKey))
            {
                horizontalAxis = -1;
                moving = true;
                if (canFlip)
                {
                    spriteRenderer.flipX = true;
                }
            }
            if (!Input.GetKey(leftKey) && Input.GetKey(rightKey))
            {
                horizontalAxis = 1;
                moving = true;
                if (canFlip)
                {
                    spriteRenderer.flipX = false;
                }
            }
            //Vertical
            if (Input.GetKey(downKey) && !Input.GetKey(upKey))
            {
                verticalAxis = -1;
                moving = true;
            }
            if (!Input.GetKey(downKey) && Input.GetKey(upKey))
            {
                verticalAxis = 1;
                moving = true;
            }
        }

        //Interact
        interacting = false;
        if (Input.GetKeyDown(interactKey))
        {
            interacting = true;
        }
    }

    void Move()
    {
        if (moving)
        {
            speed += acc;
        }
        else
        {
            speed -= dec;
        }
        speed = Mathf.Clamp(speed, 0, maxSpeed);
        vel.x = horizontalAxis;
        vel.y = verticalAxis;
        vel = vel.normalized * speed;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (nextSceneOnGoalCollide)
        {
            if (collision.CompareTag("Goal"))
            {
                SceneManager.LoadSceneAsync(nextScene);
            }
        }
    }
}
