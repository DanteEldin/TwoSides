using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendMove : MonoBehaviour
{
    //Variables
    protected bool moveNow = false;
    protected Vector2 currentPointToMoveTo;
    public float moveSpeed = 1.5f;
    public GameObject friendMoveTo;
    int currentPointCount = 1;

    //Events
    public delegate void OnPointReachedMethod(int methodNumber);
    public static OnPointReachedMethod PointReachedMethod;

    // Update
    protected virtual void Update()
    {
        Move();
    }

    //Move
    protected virtual void Move()
    {
        if (moveNow)
        {
            transform.position = Vector2.MoveTowards(transform.position, currentPointToMoveTo, moveSpeed * Time.deltaTime);
            if (transform.position == new Vector3(currentPointToMoveTo.x, currentPointToMoveTo.y, transform.position.z))
            {
                moveNow = false;
                PointReachedMethod?.Invoke(currentPointCount);
                currentPointCount++;
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
