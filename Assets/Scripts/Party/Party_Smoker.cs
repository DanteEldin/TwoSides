using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Party_Smoker : MonoBehaviour
{
    //Variables
    bool smoking = false;
    bool smokingTrig = true;
    float timer;
    public float smokeTime = 0.5f;
    public float betweenTime = 1.5f;

    //Components
    Sprite betweenSprite;
    public Sprite smokeSprite;
    SpriteRenderer spriteRenderer;

    // Start
    void Start()
    {
        timer = betweenTime;
        spriteRenderer = GetComponent<SpriteRenderer>();
        betweenSprite = spriteRenderer.sprite;
    }

    // Update
    void Update()
    {
        //timer
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
        //change sprite
        if (!smoking)
        {
            if (smokingTrig)
            {
                smokingTrig = false;
                timer = betweenTime;
            }
            //start smoking
            if (timer <= 0)
            {
                smoking = true;
                spriteRenderer.sprite = smokeSprite;
            }
        }
        else
        {
            if (!smokingTrig)
            {
                smokingTrig = true;
                timer = smokeTime;
            }
            //stop smoking
            if (timer <= 0)
            {
                smoking = false;
                timer = betweenTime;
                spriteRenderer.sprite = betweenSprite;
            }
        }
    }
}
