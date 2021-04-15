using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool_Ball : MonoBehaviour
{
    //Variables
    public int ballType = 0;
    bool sunk = false;
    float minVelocity = 0.8f;
    internal bool rolling = false;
    Vector3 startPosition;

    //Components
    Rigidbody2D rbody;
    Pool_GameHandler gameHandler;
    [SerializeField] AudioHandler sfxSinkBall;
    [SerializeField] AudioHandler sfxMissBall;

    #region EventListeners

    private void OnEnable()
    {
        Pool_GameHandler.PoolGameReset += ResetPosition;
    }

    private void OnDisable()
    {
        Pool_GameHandler.PoolGameReset -= ResetPosition;
    }

    #endregion EventListeners

    // Start
    void Start()
    {
        startPosition = transform.position;
        rbody = GetComponent<Rigidbody2D>();
        GameObject handlerObj = GameObject.Find("game handler");
        gameHandler = handlerObj.GetComponent<Pool_GameHandler>();
    }

    // Update
    void Update()
    {
        StopMovement();
    }

    //Stop Movement
    void StopMovement()
    {
        float velX = Mathf.Abs(rbody.velocity.x);
        float velY = Mathf.Abs(rbody.velocity.y);
        float combinedVel = velX + velY;

        if (combinedVel < minVelocity)
        {
            rbody.velocity = Vector2.zero;
            rolling = false;
        }
    }

    //Land in Hole
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Mark"))
        {
            if (!sunk)
            {
                //regular ball
                if (ballType == 0)
                {
                    //sink ball
                    sunk = true;
                    gameHandler.ballsToSink--;
                    transform.position = new Vector3(200, 200, 0);
                    //cheer text
                    TextHandler.TextWrite?.Invoke(TextHandler.currentCheerText);
                    TextHandler.currentCheerText++;
                    //audio
                    sfxSinkBall.PlayAudio();
                }
                //8-ball
                else if (ballType == 1)
                {
                    sunk = true;
                    //reset game
                    if (gameHandler.ballsToSink != 0)
                    {
                        gameHandler.ballsToSink = gameHandler.baseBallsToSink;
                        Pool_GameHandler.PoolGameReset?.Invoke();
                        //cheer text
                        TextHandler.TextWrite?.Invoke(gameHandler.eightBallCheerText);
                        //audio
                        sfxMissBall.PlayAudio();
                    }
                    //win game
                    else
                    {
                        Destroy(gameObject);
                        //cheer text
                        TextHandler.TextWrite?.Invoke(gameHandler.winCheerText);
                        //audio
                        sfxSinkBall.PlayAudio();
                    }
                }
                //white ball
                else
                {
                    //reset white ball
                    ResetPosition();
                }
            }
        }
    }

    //Reset Position
    void ResetPosition()
    {
        sunk = false;
        rbody.velocity = new Vector2(0, 0);
        transform.position = startPosition;
    }
}
