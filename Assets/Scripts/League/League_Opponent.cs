using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class League_Opponent : MonoBehaviour
{
    //Variables
    public int direction = -1;
    internal GameObject markedBy;

    //Movement
    float moveTimer = 0f;
    float moveCooldown = 1.2f;
    int moveDirection = 1;
    float moveSpeed = 3.2f;
    Vector2 vel;

    //Attack
    internal int health = 100;

    float attackTimer = 0f;
    float attackCooldown = 0.9f;
    float attackSpriteTime = 0.25f;
    float attackOffsetX = 1.0f;
    float attackOffsetY = -0.2f;
    public GameObject attackPrefab;
    public GameObject boomPrefab;
    public Sprite baseSprite;
    public Sprite attackSprite;

    //Components
    Rigidbody2D rbody;
    SpriteRenderer spriteRenderer;


    //Start
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //Update
    void Update()
    {
        Move();
        Attack();
        CheckIfDead();

        //Parse movement
        rbody.velocity = new Vector2(vel.x, vel.y);
    }

    //Move
    void Move()
    {
        moveTimer -= Time.deltaTime;
        //Change Move Direction
        if (moveTimer <= 0)
        {
            moveDirection *= -1;
            moveTimer = moveCooldown;
        }

        vel.y = moveSpeed * moveDirection;
    }

    //Attack
    void Attack()
    {
        bool attackAble = false;
        //Attack Timer
        if (attackTimer <= attackSpriteTime && !attackAble)
        {
            //change sprite
            spriteRenderer.sprite = baseSprite;
        }
        if (attackTimer <= 0)
        {
            attackAble = true;
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }

        //Shoot Blast
        if (attackAble)
        {
            attackTimer = attackCooldown;
            GameObject blast = Instantiate(attackPrefab,
                new Vector3(transform.position.x + (attackOffsetX * direction), transform.position.y + attackOffsetY, transform.position.z), transform.rotation);

            blast.GetComponent<League_Blast>().creator = gameObject;
            blast.GetComponent<League_Blast>().direction = direction;

            //change sprite
            spriteRenderer.sprite = attackSprite;
        }
    }

    //Check if Dead
    void CheckIfDead()
    {
        if (health <= 0)
        {
            Instantiate(boomPrefab, transform.position, transform.rotation);
            Destroy(markedBy);
            Destroy(gameObject);
            //write text
            TextHandler.TextWrite(1);
        }
    }
}
