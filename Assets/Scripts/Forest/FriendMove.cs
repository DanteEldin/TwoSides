using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendMove : MonoBehaviour
{
    //Variables
    bool moveNow = false;
    Vector2 currentPointToMoveTo;
    public float moveSpeed = 1.5f;

    //Components
    public GameObject friendMoveTo;

    // Update
    void Update()
    {
        Move();
    }

    //Move
    void Move()
    {
        if (moveNow)
        {
            transform.position = Vector2.MoveTowards(transform.position, currentPointToMoveTo, moveSpeed * Time.deltaTime);
            if (transform.position == new Vector3(currentPointToMoveTo.x, currentPointToMoveTo.y, transform.position.z))
            {
                moveNow = false;
            }
        }
    }

    //Move To Point
    public void MoveToPoint(Vector2 position)
    {
        moveNow = true;
        currentPointToMoveTo = position;
    }
}
