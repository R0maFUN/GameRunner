using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    Transform playerTransform;
    [SerializeField] public Vector3 offset;
    bool clipped = false;
    bool startClipping = false;
    public float clippingSpeed = 3f;

    public void ClipCameraToPlayer()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        startClipping = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (startClipping)
        {
            Vector3 tmpOffset = offset;
            tmpOffset.y -= Mathf.Clamp(playerTransform.position.y * 0.25f, 0.5f, 3);
            tmpOffset.z -= Mathf.Clamp(playerTransform.position.y * 0.25f, 0.5f, 3);
            float step = clippingSpeed * Time.deltaTime;

            transform.position = Vector3.MoveTowards(transform.position, playerTransform.position + tmpOffset, step);

            Vector3 currentRotation = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, transform.eulerAngles.z);
            Vector3 targetRotation = new Vector3(Mathf.Clamp(playerTransform.position.y * 1f, 5, 20), 0, 0);
            transform.eulerAngles = Vector3.MoveTowards(currentRotation, targetRotation, step * 5);
            if (transform.position == playerTransform.position + tmpOffset && transform.eulerAngles == targetRotation)
            {
                clipped = true;
                startClipping = false;
            }
        }
        if (clipped)
        {
            Vector3 tmpOffset = offset;
            tmpOffset.y -= Mathf.Clamp(playerTransform.position.y * 0.25f, 0.5f, 3);
            tmpOffset.z -= Mathf.Clamp(playerTransform.position.y * 0.25f, 0.5f, 3);
            transform.position = playerTransform.position + tmpOffset;
            //Debug.Log("currentRotation = " + transform.eulerAngles);
            //Debug.Log("targetRotation = " + new Vector3(Mathf.Clamp(playerTransform.position.y * 1f, 0, 20), 0, 0));
            transform.eulerAngles = new Vector3(Mathf.Clamp(playerTransform.position.y * 2f, 5, 20), 0, 0);
        }
    }
}
