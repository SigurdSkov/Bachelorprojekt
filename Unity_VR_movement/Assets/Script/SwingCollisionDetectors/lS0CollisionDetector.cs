using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lS0CollisionDetector : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "controller")
            other.transform.parent.parent.parent.parent.GetComponent<Armswing>().lS0Collision = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "controller")
            other.transform.parent.parent.parent.parent.GetComponent<Armswing>().lS0Collision = false;
    }
}
