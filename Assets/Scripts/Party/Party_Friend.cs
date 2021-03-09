using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party_Friend : MonoBehaviour
{
    //Variables
    bool moveNow = false;
    Vector2 currentPointToMoveTo;
    float moveSpeed = 3.0f;
    bool hadFirstConversation = false;

    int cheerTextNumber = 1;
    int cheer2TextNumber = 2;

    //Components
    SpriteRenderer spriteRenderer;
    public Sprite noGlassSprite;
    public Sprite playerGlassSprite;
    GameObject player;
    public GameObject friendMoveTo;

    #region EventListeners

    private void OnEnable()
    {
        Party_Interacts.PartyMoveTo += MoveToPoint;
        TextHandler.TextEndMethod += TextEnd;
    }

    private void OnDisable()
    {
        Party_Interacts.PartyMoveTo -= MoveToPoint;
        TextHandler.TextEndMethod -= TextEnd;
    }

    #endregion EventListeners

    //Start
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.Find("player");
    }

    //Update
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
                if (!hadFirstConversation)
                {
                    hadFirstConversation = true;
                    spriteRenderer.sprite = noGlassSprite;
                    player.GetComponent<SpriteRenderer>().sprite = playerGlassSprite;
                    //write text
                    TextHandler.TextWrite?.Invoke(cheerTextNumber);
                }
            }
        }
    }

    //Move To Point
    void MoveToPoint(Vector2 position)
    {
        moveNow = true;
        currentPointToMoveTo = position;
    }

    //Text End
    void TextEnd(int methodNumber)
    {
        if (methodNumber == 1)
        {
            player.GetComponent<Movement>().canMove = true;
            spriteRenderer.flipX = true;
            MoveToPoint(friendMoveTo.transform.position);
            //write text
            TextHandler.TextWrite?.Invoke(cheer2TextNumber);
        }
    }
}