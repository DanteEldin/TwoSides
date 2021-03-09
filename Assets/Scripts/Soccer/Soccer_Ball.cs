using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soccer_Ball : MonoBehaviour
{
    public bool gravityEnabled = false;
    float baseGravity = 0.32f;
    Vector2 opponentShot = new Vector2(-10, 6);
    bool somethingHit = false;

    int goalCheerText = 1;
    int saveCheerText = 2;
    int missCheerText = 3;

    //Components
    Rigidbody2D rbody;

    // Start
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        rbody.gravityScale = 0;
        Physics2D.IgnoreLayerCollision(9, 10, true);
        Physics2D.IgnoreLayerCollision(8, 11, true);
        Physics2D.IgnoreLayerCollision(8, 12, true);
    }

    // Update
    void Update()
    {
        if (gravityEnabled)
        {
            rbody.gravityScale = baseGravity;
        }
        else
        {
            rbody.gravityScale = 0;
        }
    }

    //Collision
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            gravityEnabled = true;
        }
        else if (collision.collider.CompareTag("Mark"))
        {
            gravityEnabled = false;
        }
        else if (collision.collider.CompareTag("Enemy") && collision.collider.GetComponent<Soccer_Opponent>().chaseBall)
        {
            collision.collider.GetComponent<Soccer_Opponent>().chaseBall = false;
            Physics2D.IgnoreLayerCollision(9, 8, true);
            rbody.velocity = opponentShot;
            //save text
            TextHandler.TextWrite?.Invoke(saveCheerText);
        }
    }

    //Collision Trigger
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!somethingHit)
        {
            if (collision.CompareTag("Wall"))
            {
                somethingHit = true;
                //stop opponent movement
                GameObject opp = GameObject.Find("opponent");
                opp.GetComponent<Soccer_Opponent>().chaseBall = false;

                //goal text
                TextHandler.TextWrite?.Invoke(missCheerText);
            }
            else if (collision.CompareTag("Goal"))
            {
                somethingHit = true;
                //stop opponent movement
                GameObject opp = GameObject.Find("opponent");
                opp.GetComponent<Soccer_Opponent>().chaseBall = false;

                //goal text
                TextHandler.TextWrite?.Invoke(goalCheerText);
            }
        }
    }
}
