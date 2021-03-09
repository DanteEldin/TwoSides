using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextHandler : MonoBehaviour
{
    //Variables
    internal static int currentCheerText = 1;

    //Events
    public delegate void OnTextWrite(int textNumber);
    public static OnTextWrite TextWrite;

    public delegate void OnTextEndMethod(int methodNumber);
    public static OnTextEndMethod TextEndMethod;

    // Start
    void Start()
    {
        currentCheerText = 1;
    }
}
