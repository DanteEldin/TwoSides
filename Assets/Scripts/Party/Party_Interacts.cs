using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party_Interacts : MonoBehaviour
{
    //Variables
    bool markHit = false;
    float friendOffsetX = -2.25f;

    //Events
    public delegate void OnPartyMoveTo(Vector2 moveTo);
    public static OnPartyMoveTo PartyMoveTo;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mark") && !markHit)
        {
            markHit = true;
            GetComponent<Movement>().canMove = false;

            Vector3 pos = new Vector3(transform.position.x + friendOffsetX, transform.position.y, transform.position.z);
            PartyMoveTo?.Invoke(pos);
        }
        if (collision.CompareTag("Goal"))
        {
            //next scene
        }
    }
}
