using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repulsive : MonoBehaviour
{
    [SerializeField] float xPower = 5f;
    [SerializeField] float yPower = 10f;
    [SerializeField] float zPower = 3f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            rb.AddForce(other.transform.position.x > gameObject.transform.position.x ? xPower : -1 * xPower, yPower, zPower, ForceMode.Impulse);
        }
    }
}
