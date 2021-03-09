using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class League_Mark : MonoBehaviour
{
    //Variables
    internal bool playerMark = true;
    internal GameObject creator;
    internal GameObject target;
    public float duration = 0.4f;

    float lifeTimer = 0f;
    float offsetY = 1.2f;

    //Start
    void Start()
    {
        lifeTimer = duration;
    }

    //Update
    void Update()
    {
        Move();
        Effect();
        Timer();
    }

    //Move
    void Move()
    {
        transform.position = new Vector3(target.transform.position.x, target.transform.position.y + offsetY, target.transform.position.z);
    }

    //Timer
    void Timer()
    {
        lifeTimer -= Time.deltaTime;
        if (lifeTimer <= 0)
        {
            if (!playerMark)
            {
                target.GetComponent<Movement>().canMove = true;
            }
            else
            {
                creator.GetComponent<Player_League>().canAttack = true;
            }
            Destroy(gameObject);
        }
    }

    //Effect
    void Effect()
    {
        //Player Mark
        if (playerMark)
        {
            creator.GetComponent<Player_League>().canAttack = false;
            //Kick Attack
            if (creator.GetComponent<Movement>().interacting && creator.GetComponent<Movement>().canMove)
            {
                creator.GetComponent<Player_League>().kickAttackMove = true;
                creator.GetComponent<Player_League>().kickTarget = target;
            }
        }
        //Enemy Mark
        else
        {
            //stop player movement
            target.GetComponent<Movement>().standardMovement = true;
            target.GetComponent<Movement>().canMove = false;
            target.GetComponent<Player_League>().kickAttackMove = false;
            target.GetComponent<Player_League>().canAttack = true;
        }
    }
}
