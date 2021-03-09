using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class General_TextMethods : MonoBehaviour
{
    public int cheerTextNumber = 2;

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

    void TextEnd(int methodNumber)
    {
        switch (methodNumber)
        {
            //just write
            case 1:
                TextHandler.TextWrite?.Invoke(cheerTextNumber);
                break;
            //add 1
            case 2:
                TextHandler.TextWrite?.Invoke(cheerTextNumber);
                cheerTextNumber++;
                break;
            //default
            default:
                TextHandler.TextWrite?.Invoke(cheerTextNumber);
                break;
        }
    }
}
