using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendMove_Multiple : FriendMove
{
    //Variables
    [SerializeField] int currentlyMovingTo = 1;
    bool nextMove = false;

    //Components
    public List<GameObject> pointsToMoveTo = new List<GameObject>();

    //Move
    protected override void Move()
    {
        if (moveNow)
        {
            transform.position = Vector2.MoveTowards(transform.position, currentPointToMoveTo, moveSpeed * Time.deltaTime);
            if (transform.position == new Vector3(currentPointToMoveTo.x, currentPointToMoveTo.y, transform.position.z))
            {
                moveNow = false;
                nextMove = true;

            }
        }
        if (nextMove)
        {
            if (currentlyMovingTo <= pointsToMoveTo.Count)
            {
                MoveToPoint(new Vector2(pointsToMoveTo[currentlyMovingTo].transform.position.x, pointsToMoveTo[currentlyMovingTo].transform.position.y));
                currentlyMovingTo++;
                nextMove = false;
            }
        }
    }
}
