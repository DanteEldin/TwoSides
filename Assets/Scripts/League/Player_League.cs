using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_League : MonoBehaviour
{
    bool interacting;
    public int direction = 1;
    internal GameObject markedBy;

    //League
    internal int health = 100;

    internal bool canAttack = true;
    bool attackAble = true;
    float attackTimer = 0f;
    float attackCooldown = 1.2f;
    float attackOffsetX = 1.0f;
    float attackOffsetY = -0.2f;
    internal bool kickAttackMove = false;
    internal GameObject kickTarget;
    float kickAttackMoveSpeed = 16.0f;

    public GameObject attackPrefab;
    public Sprite baseSprite;
    public Sprite attackSprite;

    //Components
    SpriteRenderer spriteRenderer;


    //Start
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    //Update
    void Update()
    {
        interacting = GetComponent<Movement>().interacting;

        Interact();
        if (kickAttackMove)
        {
            KickAttackMove();
        }
    }

    //Interact
    void Interact()
    {
        attackAble = false;
        //Attack Timer
        if (attackTimer <= 0)
        {
            attackAble = true;

            //change sprite
            spriteRenderer.sprite = baseSprite;
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }

        //Shoot Blast
        if (interacting && canAttack)
        {
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
    }

    //Kick Attack Move
    void KickAttackMove()
    {
        canAttack = false;
        GetComponent<Movement>().standardMovement = false;
        GetComponent<Movement>().canMove = false;

        if (kickTarget != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, kickTarget.transform.position, kickAttackMoveSpeed * Time.deltaTime);
            //change sprite
            spriteRenderer.sprite = baseSprite;
        }
    }

    //Hit Enemy With Kick
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            if (kickAttackMove)
            {
                collision.GetComponent<League_Opponent>().health = 0;
                kickAttackMove = false;
                canAttack = true;
                GetComponent<Movement>().standardMovement = true;
                GetComponent<Movement>().canMove = true;
            }
        }
    }
}
