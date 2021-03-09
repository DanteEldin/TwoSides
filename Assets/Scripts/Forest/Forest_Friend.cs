using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Forest_Friend : MonoBehaviour
{
    int textCheerNumber = 3;

    void Start()
    {
        Physics2D.IgnoreLayerCollision(8, 11);
    }

    //Collide with Mark
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mark"))
        {
            TextHandler.TextWrite(textCheerNumber);
        }
    }
}
