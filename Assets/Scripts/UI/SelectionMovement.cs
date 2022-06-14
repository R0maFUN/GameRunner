using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionMovement : MonoBehaviour
{
    private Vector3 newPosition = new Vector3();
    public float speed = 200f;
    private bool move = false;

    // Update is called once per frame
    void Update()
    {
        if (newPosition != transform.position && move)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, newPosition, step);
        }
        else if (newPosition == transform.position && move)
        {
            move = false;
        }

        transform.Rotate(0f, 10f * Time.deltaTime, 0f);
    }

    public void MoveTo(Vector3 position)
    {
        move = true;
        newPosition = position;
    }
}
