using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Soccer_Opponent : MonoBehaviour
{
    //Variables
    internal bool chaseBall = true;
    float moveSpeed = 2.5f;
    GameObject soccerBall;


    // Start
    void Start()
    {
        soccerBall = GameObject.Find("soccer ball");
    }

    // Update
    void Update()
    {

        //move towards ball
        if (chaseBall)
        {
            transform.position = Vector2.MoveTowards(transform.position, soccerBall.transform.position, moveSpeed * Time.deltaTime);
        }
    }
}
