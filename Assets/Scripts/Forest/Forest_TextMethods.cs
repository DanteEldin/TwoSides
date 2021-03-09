using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest_TextMethods : MonoBehaviour
{
    int cheerTextNumber = 2;
    int cheerText2Number = 4;
    GameObject player;
    public GameObject friend1;
    public GameObject friend2;
    float runSpeed = 3.5f;

    #region EventListeners

    private void OnEnable()
    {
        TextHandler.TextEndMethod += TextEnd;
    }

    private void OnDisable()
    {
        TextHandler.TextEndMethod -= TextEnd;
    }

    #endregion EventListeners

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("player");
        player.GetComponent<Movement>().canMove = false;
    }

    //Text End
    void TextEnd(int methodNumber)
    {
        if (methodNumber == 1)
        {
            //write text
            TextHandler.TextWrite?.Invoke(cheerTextNumber);
        }
        else if (methodNumber == 2)
        {
            //move friends
            player.GetComponent<Movement>().canMove = true;
            friend1.GetComponent<FriendMove>().MoveToPoint(friend1.GetComponent<FriendMove>().friendMoveTo.transform.position);
            friend1.GetComponent<SpriteRenderer>().flipX = false;
            friend2.GetComponent<FriendMove>().MoveToPoint(friend2.GetComponent<FriendMove>().friendMoveTo.transform.position);
            cheerTextNumber = 3;
        }
        else if (methodNumber == 3)
        {
            //write text
            TextHandler.TextWrite?.Invoke(cheerText2Number);
        }
        else if (methodNumber == 4)
        {
            //speed up friends
            friend1.GetComponent<FriendMove>().moveSpeed = runSpeed;
            friend2.GetComponent<FriendMove>().moveSpeed = runSpeed;
        }
    }
}
