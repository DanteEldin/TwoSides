using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mates_TextMethods : MonoBehaviour
{
    //Variables
    public GameObject friendMoveTo;
    public GameObject friendMoveTo2;
    //Components
    SpriteRenderer spriteRenderer;
    FriendMove friendMove;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        friendMove = GetComponent<FriendMove>();
    }

    #region EventListeners

    private void OnEnable()
    {
        TextHandler.TextEndMethod += TextEnd;
        FriendMove.PointReachedMethod += PointReached;
    }

    private void OnDisable()
    {
        TextHandler.TextEndMethod -= TextEnd;
        FriendMove.PointReachedMethod -= PointReached;
    }

    #endregion EventListeners

    void PointReached(int methodNumber)
    {
        switch (methodNumber)
        {
            //move to door
            case 1:
                friendMove.moveSpeed = 1.5f;
                spriteRenderer.flipX = false;
                friendMove.MoveToPoint(friendMoveTo2.transform.position);
                break;
            //default
            default:
                break;
        }
    }

    void TextEnd(int methodNumber)
    {
        switch (methodNumber)
        {
            //hop off chair
            case 1:
                friendMove.MoveToPoint(friendMoveTo.transform.position);
                break;
            case 2:
                break;
            //default
            default:
                break;
        }
    }
}
