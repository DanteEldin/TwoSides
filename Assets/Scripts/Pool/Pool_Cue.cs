using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool_Cue : MonoBehaviour
{
    //Variables
    KeyCode upKey = KeyCode.W;
    KeyCode downKey = KeyCode.S;
    KeyCode interactKey = KeyCode.Space;

    bool canUse = true;
    int axis = 0;
    float rotationSpeed = 50f;
    float shootForce = 10f;

    internal GameObject whiteBall;

    //Start
    void Start()
    {
        
    }

    //Update
    void Update()
    {
        if (canUse)
        {
            GetInput();
            Rotate();
            ShootBall();
        }
    }

    //Get Input
    void GetInput()
    {
        axis = 0;
        if (Input.GetKey(upKey) && !Input.GetKey(downKey))
        {
            axis = -1;
        }
        if (!Input.GetKey(upKey) && Input.GetKey(downKey))
        {
            axis = 1;
        }
    }

    //Rotate
    void Rotate()
    {
        transform.RotateAround(whiteBall.transform.position, new Vector3(0, 0, 1) * axis, rotationSpeed * Time.deltaTime);
    }

    //Shoot Ball
    void ShootBall()
    {
        if (Input.GetKeyDown(interactKey))
        {
            whiteBall.GetComponent<Rigidbody2D>().velocity = transform.TransformDirection(new Vector3(1, 0, 0) * shootForce);
            whiteBall.GetComponent<Pool_Ball>().rolling = true;
            whiteBall.GetComponent<Pool_WhiteBall>().cueSpawned = false;
            Destroy(gameObject);
        }
    }
}
