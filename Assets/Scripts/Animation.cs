using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    Animator animator;
    public float animationSpeed = 1f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.speed = animationSpeed;
    }

    void DestroyThis()
    {
        Destroy(gameObject);
    }
}
