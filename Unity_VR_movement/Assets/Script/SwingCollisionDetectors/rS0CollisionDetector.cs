using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rS0CollisionDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "controller")
            other.transform.parent.parent.parent.parent.GetComponent<Armswing>().rS0Collision = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "controller")
            other.transform.parent.parent.parent.parent.GetComponent<Armswing>().rS0Collision = false;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "controller")
            collision.transform.parent.parent.parent.GetComponent<Armswing>().rS0Collision = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.transform.tag == "controller")
            collision.transform.parent.parent.parent.GetComponent<Armswing>().rS0Collision = true;
    }
}
