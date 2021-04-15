using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool_GameHandler : MonoBehaviour
{
    //Variables
    public int baseBallsToSink = 2;
    internal int ballsToSink;
    internal int eightBallCheerText = 3;
    internal int winCheerText = 4;

    //Events
    public delegate void OnPoolGameReset();
    public static OnPoolGameReset PoolGameReset;

    #region EventListeners

    private void OnEnable()
    {
        PoolGameReset += ResetVars;
    }

    private void OnDisable()
    {
        PoolGameReset -= ResetVars;
    }

    #endregion EventListeners

    // Start
    void Start()
    {
        ballsToSink = baseBallsToSink;
    }

    //Reset Vars
    void ResetVars()
    {
        TextHandler.currentCheerText = 1;
    }
}
