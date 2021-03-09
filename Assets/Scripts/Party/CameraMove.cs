using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [Tooltip("Expecting to follow a rigidbody, this is used first.")]
    [SerializeField] protected Rigidbody2D targetRBToFollow;

    [Tooltip("Optional, if targetRBToFollow is not set, position will be fetched from this.")]
    [SerializeField] protected Transform targetToFollow;

    [Tooltip("Offset applied to target position before any additional calculations. Typically used to keep camera a fixed distance 'behind' target.")]
    [SerializeField] protected Vector3 offset = new Vector3(0, 0, -10);

    //new variables

    float smoothModifier = 7f;
    float velModifier = 0.1f;
    public float maxCamViewLeft = -30f;
    public float maxCamViewRight = 0;

    void LateUpdate()
    {
        Vector3 targetWorldPos;
        if (targetRBToFollow != null)
        {
            targetWorldPos = targetRBToFollow.transform.position;
        }
        else
        {
            targetWorldPos = targetToFollow.position;
        }
        targetWorldPos += offset;

        //get proper camera position
        float velX = targetRBToFollow.velocity.x * velModifier;
        float targetVelPosX = targetWorldPos.x + velX;
        Vector3 velPos = new Vector3(targetVelPosX, offset.y, offset.z);
        Vector3 lerpPos = Vector3.Lerp(transform.position, velPos, smoothModifier * Time.deltaTime);

        //Make sure it doesn't go past max view
        lerpPos.x = Mathf.Clamp(lerpPos.x, maxCamViewLeft, maxCamViewRight);

        transform.position = lerpPos;
    }
}
